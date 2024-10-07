namespace UserServiceApi7.Implementations;

internal class RegisteredUserService : AService<DefaultRegisteredUserDto, CreateRegisteredUserDto>, IRegisteredUserService {
	public RegisteredUserService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/registeredusers", httpClient, apiProvider) { }


	public async Task<Result<DefaultRegisteredUserDto?>> GetRegisteredUserByEmailAsync(string email, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/emails/{email}", ct);
			return await HandleResponse<DefaultRegisteredUserDto?>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<string>>> GetRolesOfUserAsync(int userId, string applicationKey, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/{userId}/applications/{applicationKey}/roles", ct);
			return await HandleResponse<List<string>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<string>>> GetRolesOfUserInCurrentAppAsync(int userId, CancellationToken ct) {
		try {
			return await GetRolesOfUserAsync(userId, Globals.UsedApplicationKey, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<DefaultRegisteredUserDto?>> AuthorizeAsync(LoginModel loginModel, CancellationToken ct) {
		try {
			var response = await HttpClient.PostAsJsonAsync($"{BaseUri}/authorize", loginModel, ct);
			return await HandleResponse<DefaultRegisteredUserDto?>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<DefaultRegisteredUserDto>>> GetPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/paginated?{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<List<DefaultRegisteredUserDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<int>> CountPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/paginated/count?{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<int>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<DefaultRegisteredUserDto>>> GetPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/locked/paginated?{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<List<DefaultRegisteredUserDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<int>> CountPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/locked/paginated/count?{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<int>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}