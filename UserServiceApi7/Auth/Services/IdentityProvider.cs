namespace UserServiceApi7.Auth.Services;

public class IdentityProvider {
	private readonly CustomAuthStateProvider _authenticationStateProvider;
	private readonly IRegisteredUserService _registeredUserService;
	private readonly ILoginUserService _loginUserService;
	private readonly ISessionService _sessionService;

	public IdentityProvider(AuthenticationStateProvider authenticationStateProvider, IRegisteredUserService registeredUserService, ILoginUserService loginUserService, ISessionService sessionService) {
		_authenticationStateProvider = authenticationStateProvider as CustomAuthStateProvider ??
		                               throw new NullReferenceException();
		_registeredUserService = registeredUserService;
		_loginUserService = loginUserService;
		_sessionService = sessionService;
	}

	public DefaultRegisteredUserDto? CurrentUser => _authenticationStateProvider.CurrentUser;

	public AuthenticationState? CachedAuthState => _authenticationStateProvider.CachedState;

	public Task<AuthenticationState> GetAuthenticationStateAsync() =>
		_authenticationStateProvider.GetAuthenticationStateAsync();

	public bool IsAuthenticated() {
		var identity = CachedAuthState?.User.Identity;
		return identity is not null && identity.IsAuthenticated;
	}

	public bool HasRole(params string[] roles) {
		var claimsPrincipal = CachedAuthState?.User;
		return claimsPrincipal is not null && roles.Any(claimsPrincipal.IsInRole);
	}

	public async Task RegisterAsync(CreateRegisteredUserDto user, string email, CancellationToken ct) {
		email = email.Trim();

		var userExists = (await _registeredUserService.GetRegisteredUserByEmailAsync(email, ct)).Value;

		if (userExists is not null)
			throw new DuplicateEmailException();

		var loginUser = (await _loginUserService.GetLoginUserByEmail(email, ct)).Value;

		if (loginUser == null)
			throw new DuplicateEmailException();

		await _registeredUserService.CreateAsync(new CreateRegisteredUserDto { Email = email, PasswordHash = user.PasswordHash, IsLocked = false }, ct);
	}


	public async Task LoginAsync(LoginModel loginModel, CancellationToken ct) {
		loginModel.Email = loginModel.Email.ToLower().Trim();

		var regUser = (await _registeredUserService.AuthorizeAsync(loginModel, ct)).Value;

		if (regUser is null)
			throw new LoginException();

		await _authenticationStateProvider.Login(loginModel);
	}

	public async Task LogoutAsync() {
		await _authenticationStateProvider.Logout();
	}

	public async Task RegisterUserAsync(CreateLoginUserDto user, CancellationToken ct) {
		var userExists = (await _loginUserService.GetLoginUserByEmail(user.Email, ct)).Value;

		if (userExists is not null)
			throw new DuplicateEmailException();

		await _loginUserService.CreateAsync(user, ct);
	}
	
	public async Task ChangePasswordAsync(ChangePasswordModel model, CancellationToken ct) {
		await _sessionService.ChangePasswordAsync(model, ct);
	}
}