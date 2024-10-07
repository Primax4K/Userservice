namespace UserServiceApi7.Interfaces;

public interface IRoleService : IService<DefaultRoleDto, CreateRoleDto> {
	Task<Result<int>> GetRoleId(ERole role, CancellationToken ct);
}