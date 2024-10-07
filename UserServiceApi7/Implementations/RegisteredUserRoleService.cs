namespace UserServiceApi7.Implementations;

internal class RegisteredUserRoleService : AResponseHandler, IRegisteredUserRoleService {
	protected readonly HttpClient HttpClient;
	protected readonly string BaseUri;

	public RegisteredUserRoleService(HttpClient httpClient, ApiProvider apiProvider) : base(httpClient, apiProvider) {
		HttpClient = httpClient;

		BaseUri = "/api/registereduserroles";
	}

	public async Task<Result<DefaultRegisteredUserRoleDto>> ReadAsync(int registeredUserId, int applicationId, int roleId, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/registeredusers/{registeredUserId}/applications/{applicationId}/roles/{roleId}", ct);
			return await HandleResponse<DefaultRegisteredUserRoleDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<DefaultRegisteredUserRoleDto>> CreateAsync(CreateRegisteredUserRoleDto createEntityDto, CancellationToken ct) {
		try {
			var response = await HttpClient.PostAsJsonAsync($"{BaseUri}", createEntityDto, ct);
			return await HandleResponse<DefaultRegisteredUserRoleDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> UpdateAsync(int registeredUserId, int applicationId, int roleId, DefaultRegisteredUserRoleDto defaultEntityDto, CancellationToken ct) {
		try {
			var response = await HttpClient.PutAsJsonAsync($"{BaseUri}/registeredusers/{registeredUserId}/applications/{applicationId}/roles/{roleId}", defaultEntityDto, ct);
			return response.IsSuccessStatusCode;
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> DeleteAsync(int registeredUserId, int applicationId, int roleId, CancellationToken ct) {
		try {
			var response = await HttpClient.DeleteAsync($"{BaseUri}/registeredusers/{registeredUserId}/applications/{applicationId}/roles/{roleId}", ct);
			return response.IsSuccessStatusCode;
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<Dictionary<int, ERole>>> GetRolesForUserAsync(string applicationKey, List<int> userIds, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/rolesofusers?applicationKey={applicationKey}&{userIds.ToQueryList("userIds")}", ct);
			return await HandleResponse<Dictionary<int, ERole>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}