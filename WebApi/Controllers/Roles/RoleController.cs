namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/roles")]
public class RoleController(IRoleRepository repository, ILogger<AController<Role, CreateRoleDto, DefaultRoleDto, DefaultRoleDto>> logger) : AController<Role, CreateRoleDto, DefaultRoleDto, DefaultRoleDto>(repository, logger) {
	[Authorize]
	[HttpGet("getroleid")]
	public async Task<ActionResult<int>> GetRoleId(ERole role, CancellationToken ct) {
		try {
			return Ok(await repository.GetRoleId(role, ct));
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