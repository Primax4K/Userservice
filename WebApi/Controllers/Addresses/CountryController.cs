namespace WebApi.Controllers.Addresses;

[ApiController]
[Route("api/countries")]
public class CountryController(ICountryRepository repository, ILogger<AController<Country, CreateCountryDto, DefaultCountryDto, DefaultCountryDto>> logger) : AController<Country, CreateCountryDto, DefaultCountryDto, DefaultCountryDto>(repository, logger) { }