namespace WebApi.Controllers.Applications;

[ApiController]
[Route("api/applications")]
public class ApplicationController(IApplicationRepository repository, ILogger<AController<Application, CreateApplicationDto, DefaultApplicationDto, DefaultApplicationDto>> logger) : AController<Application, CreateApplicationDto, DefaultApplicationDto, DefaultApplicationDto>(repository, logger) {
	[Authorize]
	[HttpGet("applicationId/{applicationKey}")]
	public async Task<ActionResult<int>> GetApplicationIdByApplicationKey(string applicationKey, CancellationToken ct) {
		try {
			return Ok(await repository.GetIdByApplicationKeyAsync(applicationKey, ct));
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