namespace Domain.Repositories.Implementations;

public class ApplicationRoleRepository(UserServiceDbContext context) : ARepository<ApplicationRole>(context), IApplicationRoleRepository { }