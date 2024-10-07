namespace UserServiceApi7.Implementations;

internal class ApplicationRoleService : AResponseHandler, IApplicationRoleService {
	protected readonly HttpClient HttpClient;
	protected readonly string BaseUri;

	public ApplicationRoleService(HttpClient httpClient, ApiProvider apiProvider) : base(httpClient, apiProvider) {
		HttpClient = httpClient;

		BaseUri = "/api/applicationroles/";
	}

	public async Task<Result<DefaultApplicationRoleDto>> ReadAsync(int applicationId, int roleId, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/applications/{applicationId}/roles/{roleId}", ct);
			return await HandleResponse<DefaultApplicationRoleDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<DefaultApplicationRoleDto>> CreateAsync(CreateApplicationRoleDto createEntityDto, CancellationToken ct) {
		try {
			var response = await HttpClient.PostAsJsonAsync($"{BaseUri}", createEntityDto, ct);
			return await HandleResponse<DefaultApplicationRoleDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> UpdateAsync(int applicationId, int roleId, DefaultApplicationRoleDto defaultEntityDto, CancellationToken ct) {
		try {
			var response = await HttpClient.PutAsJsonAsync($"{BaseUri}/applications/{applicationId}/roles/{roleId}", defaultEntityDto, ct);
			return response.IsSuccessStatusCode;
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> DeleteAsync(int applicationId, int roleId, CancellationToken ct) {
		try {
			var response = await HttpClient.DeleteAsync($"{BaseUri}/applications/{applicationId}/roles/{roleId}", ct);
			return response.IsSuccessStatusCode;
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}