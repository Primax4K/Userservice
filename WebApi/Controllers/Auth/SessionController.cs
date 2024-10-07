namespace WebApi.Controllers.Auth;

[ApiController]
[Route("api/sessions")]
public class SessionController(ISessionRepository repository, ILogger<AController<Session, CreateSessionDto, DefaultSessionDto, DefaultSessionDto>> logger) : AController<Session, CreateSessionDto, DefaultSessionDto, DefaultSessionDto>(repository, logger) {
	[AllowAnonymous]
	[HttpGet("users/{token}")]
	public async Task<ActionResult<int?>> GetUserIdByTokenAsync(string token, CancellationToken ct) {
		try {
			var result = await repository.GetUserIdByTokenAsync(token, ct);
			if (result is null)
				return NotFound();
			return Ok(result.Value);
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

	[AllowAnonymous]
	[HttpGet("tokens/{token}/users/{userId}")]
	public async Task<ActionResult<bool>> IsValidSessionAsync(string token, int userId, CancellationToken ct) {
		try {
			var result = await repository.IsValidSessionAsync(token, userId, ct);
			return Ok(result);
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
	
	[AllowAnonymous]
	[HttpGet("tokens/{token}")]
	public async Task<ActionResult<bool>> IsValidSessionAsync(string token, CancellationToken ct) {
		try {
			var valid = await repository.IsValidSessionAsync(token, ct);
			
			if (valid)
				return Ok(valid);
			
			return BadRequest();
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

	[Authorize]
	[HttpPost("deleteall/{userId}")]
	public async Task<ActionResult<bool>> DeleteAllSessionsFromUser(int userId, CancellationToken ct) {
		try {
			var now = DateTime.Now;
			await repository.DeleteAsync(s => s.UserId == userId && s.ValidUntil > now, ct);
			return Ok(true);
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