namespace WebApi.Controllers.Addresses;

[ApiController]
[Route("api/states")]
public class StateController(IStateRepository repository, ILogger<AController<State, CreateStateDto, DefaultStateDto, DefaultStateDto>> logger) : AController<State, CreateStateDto, DefaultStateDto, DefaultStateDto>(repository, logger) {
	[Authorize]
	[HttpGet]
	public async Task<ActionResult<List<DefaultStateDto>>> GetStatesOfCountry([FromQuery] int countryId, [FromQuery] string? query, CancellationToken ct) {
		try {
			var states = await repository.SearchStatesOfCountryAsync(query, countryId, ct);
			return Ok(states.Select(s => s.Adapt<DefaultStateDto>()));
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