namespace WebApi.Controllers.Users;

[ApiController]
[Route("api/registeredusers")]
public class RegisteredUserController(
	IRegisteredUserRepository repository,
	ILoginUserRepository loginUserRepository,
	IApplicationRepository applicationRepository,
	IRegisteredUserRoleRepository registeredUserRoleRepository,
	IRoleRepository roleRepository,
	ILogger<AController<RegisteredUser, CreateRegisteredUserDto, DefaultRegisteredUserDto, DefaultRegisteredUserDto>> logger)
	: AController<RegisteredUser, CreateRegisteredUserDto, DefaultRegisteredUserDto, DefaultRegisteredUserDto>(repository, logger) {
	[Authorize]
	[HttpPost]
	public override async Task<ActionResult<DefaultRegisteredUserDto>> CreateAsync(CreateRegisteredUserDto entity, CancellationToken ct) {
		try {
			var userExists = await repository.GetByEmailAsync(entity.Email, ct);

			if (userExists is not null) {
				return BadRequest("User already exists!");
			}

			var loginUser = await loginUserRepository.GetByEmailAsync(entity.Email, ct);
			if (loginUser is null) {
				return BadRequest("LoginUser does not exist!");
			}

			var user = new RegisteredUser {
				Id = loginUser.Id
			};

			var salt = BC.GenerateSalt(8);

			user.PasswordHash = BC.HashPassword(entity.PasswordHash, salt);

			user = await repository.CreateAsync(user, ct);

			if (!Request.Headers.TryGetValue("ApplicationKey", out var applicationKey)) {
				return BadRequest("Invalid ApplicationKey! [1]");
			}

			var application = await applicationRepository.FirstOrDefaultAsync(a => a.Key == applicationKey.ToString(), ct);
			if (application is null) {
				return BadRequest("Invalid ApplicationKey! [2]");
			}

			var role = await roleRepository.FirstOrDefaultAsync(r => r.Name == entity.Role, ct);
			if (role is null) {
				return BadRequest("Invalid role");
			}


			var registeredUserRole = new RegisteredUserRole {
				RegisteredUserId = user.Id,
				ApplicationId = application.Id,
				RoleId = role.Id
			};

			await registeredUserRoleRepository.CreateAsync(registeredUserRole, ct);

			return Ok(user.Adapt<DefaultRegisteredUserDto>());
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
	[HttpGet("emails/{email}")]
	public async Task<ActionResult<DefaultRegisteredUserDto>> GetByEmailAsync(string email, CancellationToken ct) {
		try {
			var user = await repository.GetByEmailAsync(email, ct);

			if (user is null) {
				return NotFound();
			}

			return Ok(user.Adapt<DefaultRegisteredUserDto>());
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
	[HttpGet("{userId}/applications/{applicationKey}/roles")]
	public async Task<ActionResult<List<string>>> GetRoleAsync(int userId, string applicationKey, CancellationToken ct) {
		try {
			var roles = await repository.GetRolesOfUserAsync(userId, applicationKey, ct);

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

	[AllowAnonymous]
	[HttpPost("authorize")]
	public async Task<ActionResult<DefaultRegisteredUserDto>> AuthorizeAsync(LoginModel loginModel, CancellationToken ct) {
		try {
			var user = await repository.AuthorizeAsync(loginModel, ct);

			if (user is null) {
				return NotFound();
			}

			return Ok(user.Adapt<DefaultRegisteredUserDto>());
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
	public async Task<ActionResult<IEnumerable<DefaultRegisteredUserDto>>> ReadRegisteredUsersPaginated([FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var registeredUsers = await repository.GetPaginatedRegisteredUsersAsync(paginatedData, ct);
			return Ok(registeredUsers.Select(r => r.Adapt<DefaultRegisteredUserDto>()));
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
			var count = await repository.CountPaginatedRegisteredUsersAsync(paginatedData, ct);
			return Ok(count);
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
	[HttpGet("locked/paginated")]
	public async Task<ActionResult<IEnumerable<DefaultRegisteredUserDto>>> ReadLockedRegisteredUsersPaginated([FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var registeredUsers = await repository.GetPaginatedLockedRegisteredUsersAsync(paginatedData, ct);
			return Ok(registeredUsers.Select(r => r.Adapt<DefaultRegisteredUserDto>()));
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
	[HttpGet("locked/paginated/count")]
	public async Task<ActionResult<int>> CountLockedRegisteredUsersPaginated([FromQuery] PaginatedData paginatedData, CancellationToken ct) {
		try {
			var count = await repository.CountPaginatedLockedRegisteredUsersAsync(paginatedData, ct);
			return Ok(count);
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
	[HttpPut("{id:int}")]
	public override async Task<ActionResult> UpdateAsync(int id, DefaultRegisteredUserDto record, CancellationToken ct) {
		try {
			var data = await repository.ReadAsync(id, ct);

			if (data is null) {
				logger.LogInformation($"Invalid Request: Entity not present - {id}");
				return NotFound();
			}

			data.IsLocked = record.IsLocked;
			
			await repository.UpdateAsync(data, ct);
			logger.LogInformation($"Updated Entity: {id}");

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
}