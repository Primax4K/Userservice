namespace UserServiceApi7.Implementations;

internal class SessionService : AService<DefaultSessionDto, CreateSessionDto>, ISessionService {
	public SessionService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/sessions", httpClient, apiProvider) { }


	public async Task<Result<int?>> GetUserIdByTokenAsync(string token, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/users/{token}", ct);
			return await HandleResponse<int?>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> IsValidSessionAsync(string token, int userId, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/tokens/{token}/users/{userId}", ct);
			return await HandleResponse<bool>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<string?>> LoginAsync(LoginModel model, CancellationToken ct) {
		try {
			var response = await HttpClient.PostAsJsonAsync($"/api/auth/login", model, ct);
			return await ReadPlain(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> IsValidSessionAsync(string token, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/tokens/{token}", ct);
			return await HandleResponse<bool>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<DefaultRegisteredUserDto>> ChangePasswordAsync(ChangePasswordModel model, CancellationToken ct) {
		try {
			var response = await HttpClient.PutAsJsonAsync($"/api/auth/changepassword", model, ct);
			return await HandleResponse<DefaultRegisteredUserDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> LogoutEverywhereAsync(int userId, CancellationToken ct) {
		try {
			var response = await HttpClient.PostAsync($"{BaseUri}/deleteall/{userId}", null, ct);
			return await HandleResponse<bool>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}