namespace UserServiceApi7.Implementations.Abstract;

public abstract class AResponseHandler : ApiMessageService {
	protected async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response, CancellationToken ct) {
		if (response.IsSuccessStatusCode) {
			var content = await response.Content.ReadFromJsonAsync<T>(cancellationToken: ct);
			if (content is not null) return content;
			return new Exception("No content");
		}

		return new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
	}

	protected AResponseHandler(HttpClient client, ApiProvider apiProvider) : base(client, apiProvider) { }
}