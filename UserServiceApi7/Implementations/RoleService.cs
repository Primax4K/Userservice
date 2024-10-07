namespace UserServiceApi7.Implementations;

internal class RoleService : AService<DefaultRoleDto, CreateRoleDto>, IRoleService {
	public RoleService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/roles", httpClient, apiProvider) { }
	public async Task<Result<int>> GetRoleId(ERole role, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/getroleid?role={(int) role}", ct);
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