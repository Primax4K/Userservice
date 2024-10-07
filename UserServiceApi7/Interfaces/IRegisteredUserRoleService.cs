namespace UserServiceApi7.Interfaces;

public interface IRegisteredUserRoleService {
	Task<Result<DefaultRegisteredUserRoleDto>> ReadAsync(int registeredUserId, int applicationId, int roleId, CancellationToken ct);
	Task<Result<DefaultRegisteredUserRoleDto>> CreateAsync(CreateRegisteredUserRoleDto createEntityDto, CancellationToken ct);
	Task<Result<bool>> UpdateAsync(int registeredUserId, int applicationId, int roleId, DefaultRegisteredUserRoleDto defaultEntityDto, CancellationToken ct);
	Task<Result<bool>> DeleteAsync(int registeredUserId, int applicationId, int roleId, CancellationToken ct);

	Task<Result<Dictionary<int, ERole>>> GetRolesForUserAsync(string applicationKey, List<int> userIds, CancellationToken ct);
}