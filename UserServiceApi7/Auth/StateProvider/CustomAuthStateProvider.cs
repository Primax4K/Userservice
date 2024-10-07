namespace UserServiceApi7.Auth.StateProvider;

public class CustomAuthStateProvider : AuthenticationStateProvider {
	private readonly HttpClient _client;
	private readonly ProtectedLocalStorage _local;
	private readonly ApiProvider _apiProvider;
	private readonly ILogger<CustomAuthStateProvider> _logger;
	private readonly ISessionService _sessionService;
	private readonly ILoginUserService _loginUserService;
	private readonly IRegisteredUserService _registeredUserService;

	private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);


	private DateTime _cachedStateTime;

	// cache the current AuthenticationState to avoid unnecessary calls to the server
	// the state is cached for 5 minutes
	public AuthenticationState? CachedState;

	public CustomAuthStateProvider(HttpClient client, ProtectedLocalStorage local, ApiProvider apiProvider, ILogger<CustomAuthStateProvider> logger, ISessionService sessionService, ILoginUserService loginUserService, IRegisteredUserService registeredUserService) {
		_client = client;
		_local = local;
		_apiProvider = apiProvider;
		_logger = logger;
		_sessionService = sessionService;
		_loginUserService = loginUserService;
		_registeredUserService = registeredUserService;
	}

	public DefaultRegisteredUserDto? CurrentUser { get; private set; }

	private static AuthenticationState Anonymous => new(new ClaimsPrincipal(new ClaimsIdentity()));

	private void SetCachedState(AuthenticationState state) {
		CachedState = state;
		_cachedStateTime = DateTime.Now;
	}

	private void ClearCache() {
		CachedState = null;
		_cachedStateTime = DateTime.MaxValue;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
		try {
			if (CachedState is not null && DateTime.Now - _cachedStateTime < _cacheDuration)
				return CachedState;
			ClearCache();

			var user = await GetUserAsync();
			if (user is null) return Anonymous;

			CurrentUser = user;

			var loginUser = (await _loginUserService.ReadAsync(user.Id, CancellationToken.None)).Value;
			var identity = new ClaimsIdentity(await GenerateClaims(loginUser, Globals.UsedApplicationKey), "GetStateType");
			var authState = new AuthenticationState(new ClaimsPrincipal(identity));

			SetCachedState(authState);
			NotifyAuthenticationStateChanged(Task.FromResult(authState));

			return authState;
		}
		catch (CryptographicException) {
			await _local.DeleteAsync("token");
			return Anonymous;
		}
		catch (InvalidOperationException) {
			return Anonymous;
		}
		catch (OperationCanceledException e) {
			_logger.LogError(e, "Error while getting authentication state");
			return Anonymous;
		}
		catch (Exception e) {
			_logger.LogError(e, "Error while getting authentication state");
			return Anonymous;
		}
	}

	private async Task<DefaultRegisteredUserDto?> GetUserAsync() {
		var token = await _local.GetAsync<string>("token");

		//login to api
		_apiProvider.Token = token.Value;

		if (token is not { Success: true, Value: not "" }) return null;
		var id = (await _sessionService.GetUserIdByTokenAsync(token.Value!, CancellationToken.None)).Value;

		if (id == null) return null;
		if (!(await _sessionService.IsValidSessionAsync(token.Value!, id.Value, CancellationToken.None)).Value) return null;

		var user = (await _registeredUserService.ReadAsync(id.Value, CancellationToken.None)).Value;
		return user.IsLocked ? null : user;
	}

	private async Task<IEnumerable<Claim>> GenerateClaims(DefaultLoginUserDto user, string applicationKey) {
		var roles = (await _registeredUserService.GetRolesOfUserAsync(user.Id, applicationKey, CancellationToken.None)).Value;

		var claims = new List<Claim> {
			new(ClaimTypes.Name, string.Join(" ", user.FirstName, user.LastName)),
			new(ClaimTypes.Email, user.Email),
		};

		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList());

		return claims;
	}

	public async Task Login(LoginModel model) {
		ClearCache();

		var regUser = (await _registeredUserService.GetRegisteredUserByEmailAsync(model.Email, CancellationToken.None)).Value;

		if (regUser is { IsLocked: true }) {
			throw new UserIsLockedException();
		}

		CurrentUser = regUser;

		var loginUser = await _loginUserService.ReadAsync(regUser!.Id, CancellationToken.None);

		if (loginUser.IsError) {
			throw loginUser.Error;
		}

		var sessionToken = await _sessionService.LoginAsync(model, CancellationToken.None);

		if (sessionToken.IsError) {
			throw sessionToken.Error;
		}

		var authState =
			new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(await GenerateClaims(loginUser.Value, Globals.UsedApplicationKey), "LoginType")));

		SetCachedState(authState);

		NotifyAuthenticationStateChanged(Task.FromResult(authState));
		await _local.SetAsync("token", sessionToken.Value!);
	}

	public async Task Logout() {
		CurrentUser = null;
		ClearCache();
		await _local.DeleteAsync("token");
		//logout to api
		_apiProvider.Token = null;
		NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
	}
}