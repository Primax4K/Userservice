namespace WebApi.Services;

public class CustomAuthenticationHandler(
	IOptionsMonitor<CustomAuthOptions> options,
	ILoggerFactory logger,
	UrlEncoder encoder,
	ISessionRepository sessionRepository,
	IUserRepository userRepo,
	IRegisteredUserRepository regUserRepo)
	: AuthenticationHandler<CustomAuthOptions>(options, logger, encoder) {
	protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
		try {
			if (!Request.Headers.TryGetValue("Auth", out var authHeader)) return AuthenticateResult.NoResult();
			if (authHeader.Count == 0) return AuthenticateResult.Fail("No authorization header provided");
			var token = authHeader[0];

			if (token is null) return AuthenticateResult.Fail("No token provided");

			var id = await sessionRepository.GetUserIdByTokenAsync(token, CancellationToken.None);
			if (id is null) return AuthenticateResult.Fail("Invalid token provided [1]");

			if (!await sessionRepository.IsValidSessionAsync(token, id.Value, CancellationToken.None))
				return AuthenticateResult.Fail("Invalid token provided [2]");

			var regUser = await regUserRepo.AuthorizeAsync(id.Value, CancellationToken.None);
			if (regUser is null) return AuthenticateResult.Fail("RegisteredUser not found");

			var user = await userRepo.ReadAsync(regUser.Id, CancellationToken.None);
			//if (user is null) return AuthenticateResult.Fail("User not found");

			var roles = regUser.RegisteredUserRoles.Select(r => r.ApplicationRole.Role.ToString()).ToArray();

			var claims = await GenerateClaims(regUser);
			var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "GetStateType"));

			return AuthenticateResult.Success(new AuthenticationTicket(principal, "GetStateType"));
		}
		catch (Exception e) {
			Logger.LogError(e, "Error while getting authentication state");
			return AuthenticateResult.Fail(e.Message);
		}
	}

	private Task<IEnumerable<Claim>> GenerateClaims(RegisteredUser user) {
		var roles = user.RegisteredUserRoles.Select(r => r.ApplicationRole.Role.Name).ToArray();
		return Task.FromResult<IEnumerable<Claim>>(roles.Select(role => new Claim(ClaimTypes.Role, role.ToEnumString()))
			.ToList());
	}
}