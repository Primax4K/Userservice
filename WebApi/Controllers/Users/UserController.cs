namespace WebApi.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserController(IUserRepository repository, ILogger<AController<User, CreateUserDto, DefaultUserDto, DefaultUserDto>> logger) : AController<User, CreateUserDto, DefaultUserDto, DefaultUserDto>(repository, logger) {
	[Authorize]
	[HttpGet("paginated")]
	public async Task<ActionResult<IEnumerable<DefaultUserDto>>> ReadRegisteredUsersPaginated([FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var registeredUsers = await repository.GetPaginatedUsersAsync(paginatedData, ct);
			return Ok(registeredUsers.Select(u => u.Adapt<DefaultUserDto>()));
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
	[HttpGet("paginated/count")]
	public async Task<ActionResult<int>> CountRegisteredUsersPaginated([FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var loginUserCount = await repository.CountPaginatedUsersAsync(paginatedData, ct);
			return Ok(loginUserCount);
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