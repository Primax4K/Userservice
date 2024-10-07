namespace Domain.Repositories.Implementations;

public class RegisteredUserRoleRepository(UserServiceDbContext context, IApplicationRepository applicationRepository) : ARepository<RegisteredUserRole>(context), IRegisteredUserRoleRepository {
	public async Task<Dictionary<int, ERole>> GetRolesForUserAsync(List<int> userIds, string applicationKey, CancellationToken ct) {
		int applicationId = (await applicationRepository.ReadAsync(a => a.Key == applicationKey, ct)).First().Id;

		return await context.RegisteredUserRoles
			.Where(rur => userIds.Contains(rur.RegisteredUserId) && rur.ApplicationId == applicationId)
			.ToDictionaryAsync(rur => rur.RegisteredUserId, rur => rur.ApplicationRole.Role.Name, ct);
	}
}