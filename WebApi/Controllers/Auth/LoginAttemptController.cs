namespace WebApi.Controllers.Auth;

[ApiController]
[Route("api/loginattempts")]
public class LoginAttemptController(ILoginAttemptRepository repository, ILogger<AController<LoginAttempt, CreateLoginAttemptDto, DefaultLoginAttemptDto, DefaultLoginAttemptDto>> logger) : AController<LoginAttempt, CreateLoginAttemptDto, DefaultLoginAttemptDto, DefaultLoginAttemptDto>(repository, logger);