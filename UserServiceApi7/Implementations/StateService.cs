namespace UserServiceApi7.Implementations;

internal class StateService : AService<DefaultStateDto, CreateStateDto>, IStateService {
	public StateService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/states", httpClient, apiProvider) { }

	public async Task<Result<List<DefaultStateDto>>> GetStatesOfCountryAsync(int countryId, string? query, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}?countryId={countryId}&query={query}", ct);
			return await HandleResponse<List<DefaultStateDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}