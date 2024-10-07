namespace UserServiceApi7.Implementations;

internal class GenderService : AService<DefaultGenderDto, CreateGenderDto>, IGenderService {
	public GenderService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/genders", httpClient, apiProvider) { }

	public async Task<Result<List<DefaultGenderDto>>> ReadAllAsync(CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}", ct);
			return await HandleResponse<List<DefaultGenderDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}