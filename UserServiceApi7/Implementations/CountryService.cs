namespace UserServiceApi7.Implementations;

internal class CountryService : AService<DefaultCountryDto, CreateCountryDto>, ICountryService {
	public CountryService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/countries", httpClient, apiProvider) { }
}