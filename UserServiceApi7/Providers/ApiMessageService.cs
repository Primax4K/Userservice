namespace UserServiceApi7.Providers;

public class ApiMessageService {
	private readonly HttpClient _client;
	private readonly ApiProvider _apiProvider;

	public ApiMessageService(HttpClient client, ApiProvider apiProvider) {
		_client = client;
		_apiProvider = apiProvider;

		InitializeClient();
		apiProvider.TokenChanged += InitializeClient;
	}

	public void InitializeClient() {
		if (_apiProvider.Token == null && _client.DefaultRequestHeaders.Contains("Auth"))
			_client.DefaultRequestHeaders.Remove("Auth");
		if (_apiProvider.Token != null && !_client.DefaultRequestHeaders.Contains("Auth"))
			_client.DefaultRequestHeaders.Add("Auth", _apiProvider.Token);
	}
}