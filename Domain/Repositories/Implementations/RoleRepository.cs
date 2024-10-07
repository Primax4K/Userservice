namespace Domain.Repositories.Implementations;

public class RoleRepository(UserServiceDbContext context) : ARepository<Role>(context), IRoleRepository {
	public async Task<int> GetRoleId(ERole role, CancellationToken ct) {
		return (await Table.FirstAsync(r => r.Name == role, ct)).Id;
	}
}