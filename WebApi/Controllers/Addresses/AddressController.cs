namespace WebApi.Controllers.Addresses;

[ApiController]
[Route("api/addresses")]
public class AddressController(IAddressRepository repository, ILogger<AController<Address, CreateAddressDto, DefaultAddressDto, DefaultAddressDto>> logger)
	: AController<Address, CreateAddressDto, DefaultAddressDto, DefaultAddressDto>(repository, logger) {
	[AllowAnonymous]
	[HttpGet("{id:int}")]
	public override async Task<ActionResult<DefaultAddressDto>> ReadAsync(int id, CancellationToken ct) {
		try {
			return await base.ReadAsync(id, ct);
		}
		catch (OperationCanceledException) {
			logger.LogError("Zeitüberschreitung der Anforderungen!");
			return StatusCode(408);
		}
		catch (Exception e) {
			logger.LogError(e, "Fehler beim Abrufen der Entität!");
			return Problem("Fehler beim Abrufen der Entität!");
		}
	}
}