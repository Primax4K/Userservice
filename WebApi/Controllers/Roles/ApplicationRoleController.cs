namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/applicationroles")]
public class ApplicationRoleController(IApplicationRoleRepository repository, ILogger<ApplicationRoleController> logger) : ControllerBase {
	[Authorize]
	[HttpGet("applications/{applicationId}/roles/{roleId}")]
	public async Task<ActionResult<DefaultApplicationRoleDto>> ReadAsync(int applicationId, int roleId, CancellationToken ct) {
		try {
			ApplicationRole? applicationRole = (await repository.ReadAsync(a => a.ApplicationId == applicationId && a.RoleId == roleId, ct)).FirstOrDefault();

			if (applicationRole is null) {
				return NotFound();
			}

			return Ok(applicationRole.Adapt<DefaultApplicationRoleDto>());
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
	[HttpPut("applications/{applicationId}/roles/{roleId}")]
	public async Task<ActionResult> UpdateAsync(int applicationId, int roleId, DefaultApplicationRoleDto entity, CancellationToken ct) {
		try {
			ApplicationRole? applicationRole = (await repository.ReadAsync(a => a.ApplicationId == applicationId && a.RoleId == roleId, ct)).FirstOrDefault();

			if (applicationRole is null) {
				return NotFound();
			}

			await repository.UpdateAsync(entity.Adapt<ApplicationRole>(), ct);
			return NoContent();
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
	[HttpDelete("applications/{applicationId}/roles/{roleId}")]
	public async Task<ActionResult> DeleteAsync(int applicationId, int roleId, CancellationToken ct) {
		try {
			ApplicationRole? applicationRole = (await repository.ReadAsync(a => a.ApplicationId == applicationId && a.RoleId == roleId, ct)).FirstOrDefault();

			if (applicationRole is null) {
				return NotFound();
			}

			await repository.DeleteAsync(applicationRole, ct);
			return NoContent();
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
	[HttpPost]
	public async Task<ActionResult<DefaultApplicationRoleDto>> CreateAsync(CreateApplicationRoleDto entity, CancellationToken ct) {
		try {
			return Ok((await repository.CreateAsync(entity.Adapt<ApplicationRole>(), ct)).Adapt<DefaultApplicationRoleDto>());
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