namespace UserServiceApi7.Interfaces;

public interface IUserService : IService<DefaultUserDto, CreateUserDto> {
	Task<Result<List<DefaultUserDto>>> GetPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<Result<int>> CountPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct);
}