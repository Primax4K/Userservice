namespace WebApi.Controllers.Users;

[ApiController]
[Route("api/loginusers")]
public class LoginUserController(ILoginUserRepository repository, ILogger<AController<LoginUserData, CreateLoginUserDto, DefaultLoginUserDto, DefaultLoginUserDto>> logger)
	: AController<LoginUserData, CreateLoginUserDto, DefaultLoginUserDto, DefaultLoginUserDto>(repository, logger) {
	[AllowAnonymous]
	[HttpPost]
	public override async Task<ActionResult<DefaultLoginUserDto>> CreateAsync(CreateLoginUserDto entity, CancellationToken ct) {
		try {
			entity.Email = entity.Email.ToLower().Trim();
			return await base.CreateAsync(entity, ct);
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

	[HttpGet("email/{email}")]
	public async Task<ActionResult<DefaultLoginUserDto>> GetLoginUserByEmail(string email, CancellationToken ct) {
		try {
			var user = await repository.GetByEmailAsync(email, ct);
			if (user is null) return NotFound();
			return Ok(user.Adapt<DefaultLoginUserDto>());
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
	[HttpGet("{id}")]
	public override async Task<ActionResult<DefaultLoginUserDto>> ReadAsync(int id, CancellationToken ct) {
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

	[Authorize]
	[HttpGet("paginated")]
	public async Task<ActionResult<IEnumerable<DefaultLoginUserDto>>> ReadLoginUsersPaginated([FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var loginUsers = await repository.GetPaginatedLoginUsersAsync(paginatedData, ct);
			return Ok(loginUsers.Select(l => l.Adapt<DefaultLoginUserDto>()).ToList());
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
	public async Task<ActionResult<int>> CountLoginUsersPaginated([FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var loginUserCount = await repository.CountPaginatedLoginUsersAsync(paginatedData, ct);
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

	[AllowAnonymous]
	[HttpGet("full")]
	public async Task<ActionResult<DefaultFullUserDto>> GetFullUserAsync(int userId, string applicationKey, CancellationToken ct) {
		try {
			var fullUser = await repository.GetFullUserAsync(userId, applicationKey, ct);
			return Ok(fullUser);
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
	[HttpGet("readrange")]
	public async Task<ActionResult<List<DefaultLoginUserDto>>> ReadRangeAsync([FromQuery] List<int> ids, CancellationToken ct) {
		try {
			return (await repository.ReadRangeAsync(ids, ct)).Select(l => l.Adapt<DefaultLoginUserDto>()).ToList();
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
	[HttpGet("paginated/excepted")]
	public async Task<ActionResult<List<DefaultLoginUserDto>>> GetExceptedUsersPaginatedAsync([FromQuery] List<int> userIds, [FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var loginUsers = await repository.GetExceptedUsersPaginatedAsync(userIds, paginatedData, ct);
			return Ok(loginUsers.Select(l => l.Adapt<DefaultLoginUserDto>()).ToList());
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
	[HttpGet("paginated/excepted/count")]
	public async Task<ActionResult<List<DefaultLoginUserDto>>> CountExceptedUsersPaginatedAsync([FromQuery] List<int> userIds, [FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var loginUserCount = await repository.CountExceptedUsersPaginatedAsync(userIds, paginatedData, ct);
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

	[Authorize]
	[HttpGet("paginated/inlist")]
	public async Task<ActionResult<IEnumerable<DefaultLoginUserDto>>> GetPaginatedUsersInList([FromQuery] List<int> userIds, [FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var loginUsers = await repository.GetPaginatedUsersInList(userIds, paginatedData, ct);
			return Ok(loginUsers.Select(l => l.Adapt<DefaultLoginUserDto>()).ToList());
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