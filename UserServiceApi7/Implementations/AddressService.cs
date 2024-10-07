namespace UserServiceApi7.Implementations;

internal class AddressService : AService<DefaultAddressDto, CreateAddressDto>, IAddressService {
	public AddressService(HttpClient httpClient, ApiProvider apiProvider) : base("/api/addresses", httpClient, apiProvider) { }
}