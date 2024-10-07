namespace UserServiceApi7.Implementations;

internal class ApplicationService : AService<DefaultApplicationDto, CreateApplicationDto>, IApplicationService {
	public ApplicationService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/applications", httpClient, apiProvider) { }

	public async Task<Result<int>> GetIdByApplicationKeyAsync(string applicationKey, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/applicationid/{applicationKey}", ct);
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