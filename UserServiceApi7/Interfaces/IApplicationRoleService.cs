namespace UserServiceApi7.Interfaces;

public interface IApplicationRoleService {
	Task<Result<DefaultApplicationRoleDto>> ReadAsync(int applicationId, int roleId, CancellationToken ct);
	Task<Result<DefaultApplicationRoleDto>> CreateAsync(CreateApplicationRoleDto createEntityDto, CancellationToken ct);
	Task<Result<bool>> UpdateAsync(int applicationId, int roleId, DefaultApplicationRoleDto defaultEntityDto, CancellationToken ct);
	Task<Result<bool>> DeleteAsync(int applicationId, int roleId, CancellationToken ct);
}