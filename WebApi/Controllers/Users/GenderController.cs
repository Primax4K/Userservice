namespace WebApi.Controllers.Users;

[ApiController]
[Route("api/genders")]
public class GenderController(IGenderRepository repository, ILogger<AController<Gender, CreateGenderDto, DefaultGenderDto, DefaultGenderDto>> logger) : AController<Gender, CreateGenderDto, DefaultGenderDto, DefaultGenderDto>(repository, logger) {
	[Authorize]
	[HttpGet]
	public async Task<ActionResult<List<DefaultGenderDto>>> ReadAllAsync(CancellationToken ct) {
		try {
			return Ok((await repository.ReadAllAsync(ct)).Select(g => g.Adapt<DefaultGenderDto>()));
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