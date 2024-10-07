namespace UserServiceApi7.Implementations;

internal class UserService : AService<DefaultUserDto, CreateUserDto>, IUserService {
	public UserService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/users", httpClient, apiProvider) { }

	public async Task<Result<List<DefaultUserDto>>> GetPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/paginated?{paginatedData.ToQueryString()}", ct);
			return await HandleResponse<List<DefaultUserDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<int>> CountPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
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
}