namespace UserServiceApi7.Implementations;

internal class LoginAttemptService : AService<DefaultLoginAttemptDto, CreateLoginAttemptDto>, ILoginAttemptService {
	public LoginAttemptService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/loginattempts", httpClient, apiProvider) { }
}