namespace Domain.Repositories.Interfaces;

public interface IRegisteredUserRoleRepository : IRepository<RegisteredUserRole> {
	Task<Dictionary<int, ERole>> GetRolesForUserAsync(List<int> userIds, string applicationKey, CancellationToken ct);
}