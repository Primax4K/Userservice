namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/registereduserroles")]
public class RegisteredUserRoleController(IRegisteredUserRoleRepository repository, ILogger<RegisteredUserRoleController> logger) : ControllerBase {
	[Authorize]
	[HttpGet("registeredusers/{registeredUserId}/applications/{applicationId}/roles/{roleId}")]
	public async Task<ActionResult<DefaultRegisteredUserRoleDto>> ReadAsync(int registeredUserId, int applicationId, int roleId, CancellationToken ct) {
		try {
			RegisteredUserRole? applicationRole = (await repository.ReadAsync(a => a.RegisteredUserId == registeredUserId && a.ApplicationId == applicationId && a.RoleId == roleId, ct)).FirstOrDefault();

			if (applicationRole is null) {
				return NotFound();
			}

			return Ok(applicationRole.Adapt<DefaultRegisteredUserRoleDto>());
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
	[HttpPut("registeredusers/{registeredUserId}/applications/{applicationId}/roles/{roleId}")]
	public async Task<ActionResult> UpdateAsync(int registeredUserId, int applicationId, int roleId, DefaultRegisteredUserRoleDto record, CancellationToken ct) {
		try {
			RegisteredUserRole? applicationRole = (await repository.ReadAsync(a => a.RegisteredUserId == registeredUserId && a.ApplicationId == applicationId && a.RoleId == roleId, ct)).FirstOrDefault();

			if (applicationRole is null) {
				return NotFound();
			}
			
			await repository.DeleteAsync(applicationRole, ct);
			
			await repository.CreateAsync(record.Adapt<RegisteredUserRole>(), ct);
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
	[HttpDelete("registeredusers/{registeredUserId}/applications/{applicationId}/roles/{roleId}")]
	public async Task<ActionResult> DeleteAsync(int registeredUserId, int applicationId, int roleId, CancellationToken ct) {
		try {
			RegisteredUserRole? applicationRole = (await repository.ReadAsync(a => a.RegisteredUserId == registeredUserId && a.ApplicationId == applicationId && a.RoleId == roleId, ct)).FirstOrDefault();

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
	public async Task<ActionResult<DefaultRegisteredUserRoleDto>> CreateAsync(CreateRegisteredUserRoleDto entity, CancellationToken ct) {
		try {
			return Ok((await repository.CreateAsync(entity.Adapt<RegisteredUserRole>(), ct)).Adapt<DefaultRegisteredUserRoleDto>());
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
	[HttpGet("rolesofusers")]
	public async Task<ActionResult<List<Dictionary<int, ERole>>>> GetRolesForUser(string applicationKey, [FromQuery] List<int> userIds, CancellationToken ct) {
		try {
			var roles = await repository.GetRolesForUserAsync(userIds, applicationKey, ct);
			return Ok(roles);
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