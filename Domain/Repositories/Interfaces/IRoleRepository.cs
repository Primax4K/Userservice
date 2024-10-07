namespace Domain.Repositories.Interfaces;

public interface IRoleRepository : IRepository<Role> {
	Task<int> GetRoleId(ERole role, CancellationToken ct);
}