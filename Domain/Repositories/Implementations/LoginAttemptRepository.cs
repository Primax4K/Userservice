namespace Domain.Repositories.Implementations;

public class LoginAttemptRepository(UserServiceDbContext context) : ARepository<LoginAttempt>(context), ILoginAttemptRepository { }