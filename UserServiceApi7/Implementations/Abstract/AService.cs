namespace UserServiceApi7.Implementations.Abstract;

internal abstract class AService<TDefaultDto, TCreateDto> : AResponseHandler, IService<TDefaultDto, TCreateDto> {
	protected readonly HttpClient HttpClient;
	protected readonly string BaseUri;

	protected AService(string baseUri, HttpClient httpClient, ApiProvider apiProvider) : base(httpClient, apiProvider) {
		HttpClient = httpClient;
		BaseUri = baseUri;
	}

	protected async Task<string?> ReadPlain(HttpResponseMessage response, CancellationToken ct) {
		if (!response.IsSuccessStatusCode) return null;
		var content = await response.Content.ReadAsStringAsync(ct);
		return content;
	}

	public async Task<Result<TDefaultDto>> ReadAsync(int id, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/{id}", ct);
			return await HandleResponse<TDefaultDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<TDefaultDto>> CreateAsync(TCreateDto createEntityDto, CancellationToken ct) {
		try {
			var response = await HttpClient.PostAsJsonAsync($"{BaseUri}", createEntityDto, ct);
			return await HandleResponse<TDefaultDto>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> UpdateAsync(int id, TDefaultDto defaultEntityDto, CancellationToken ct) {
		try {
			var response = await HttpClient.PutAsJsonAsync($"{BaseUri}/{id}", defaultEntityDto, ct);
			return response.IsSuccessStatusCode;
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<bool>> DeleteAsync(int id, CancellationToken ct) {
		try {
			var response = await HttpClient.DeleteAsync($"{BaseUri}/{id}", ct);
			return response.IsSuccessStatusCode;
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}

	public async Task<Result<List<TDefaultDto>>> SearchAsync(string? query, CancellationToken ct) {
		try {
			var response = await HttpClient.GetAsync($"{BaseUri}/search?searchString={query}", ct);
			return await HandleResponse<List<TDefaultDto>>(response, ct);
		}
		catch (HttpRequestException httpEx) {
			return httpEx;
		}
		catch (Exception ex) {
			return ex;
		}
	}
}