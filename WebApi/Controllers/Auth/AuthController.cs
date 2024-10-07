namespace WebApi.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IRegisteredUserRepository registeredUserRepository, ISessionRepository sessionRepository, ILogger<AuthController> logger) : ControllerBase {
	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<ActionResult<string>> Login(LoginModel loginModel) {
		try {
			loginModel.Email = loginModel.Email.Trim().ToLower();

			var regUser = await registeredUserRepository.AuthorizeAsync(loginModel, CancellationToken.None);

			if (regUser is null) return BadRequest();
			if (regUser.IsLocked) return BadRequest();

			var salt = BC.GenerateSalt(8);
			var token = BC.HashPassword(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), salt);

			token = token.Replace("$", "").Replace("/", "").Replace("%", "")[..50];

			await sessionRepository.CreateAsync(new Session() {
				Token = token,
				CreatedAt = DateTime.Now,
				ValidUntil = DateTime.Now.AddMonths(1),
				UserId = regUser.Id
			}, CancellationToken.None);
			return Ok(token);
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
	[HttpPut("ChangePassword")]
	public async Task<ActionResult> ChangePassword(ChangePasswordModel model) {
		try {
			var user = await registeredUserRepository.ReadAsync(model.RegisteredUser.Id, CancellationToken.None);
			if (user is null) return BadRequest();
			
			var salt = BC.GenerateSalt(8);
			model.NewPasswordHash = BC.HashPassword(model.NewPasswordHash, salt);
			
			user.PasswordHash = model.NewPasswordHash;
			await registeredUserRepository.UpdateAsync(user, CancellationToken.None);
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
}