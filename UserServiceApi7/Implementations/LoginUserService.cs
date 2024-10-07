namespace UserServiceApi7.Implementations;

internal class LoginUserService : AService<DefaultLoginUserDto, CreateLoginUserDto>, ILoginUserService {
	public LoginUserService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/loginusers", httpClient, apiProvider) { }


	public async Task<Result<DefaultLoginUserDto?>> GetLoginUserByEmail(string email, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/email/{email}", ct);
			return await HandleResponse<DefaultLoginUserDto?>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<DefaultLoginUserDto>>> GetPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/paginated?{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<List<DefaultLoginUserDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<int>> CountPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
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

	public async Task<Result<DefaultFullUserDto>> GetFullUserAsync(int userId, string applicationKey, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/full?userId={userId}&applicationKey={applicationKey}", ct);
			return await HandleResponse<DefaultFullUserDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<DefaultLoginUserDto>>> ReadRangeAsync(List<int> ids, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/readrange?{ids.ToQueryList("ids")}", ct);
			return await HandleResponse<List<DefaultLoginUserDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<DefaultLoginUserDto>>> GetExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/paginated/excepted?{userIds.ToQueryList("userIds")}&{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<List<DefaultLoginUserDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<int>> CountExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/paginated/excepted/count?{userIds.ToQueryList("userIds")}&{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<int>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<DefaultLoginUserDto>>> GetPaginatedUsersInList(List<int> userIds, PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/paginated/inlist?{userIds.ToQueryList("userIds")}&{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<List<DefaultLoginUserDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}