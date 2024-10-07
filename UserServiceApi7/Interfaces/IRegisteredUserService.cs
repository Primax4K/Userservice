namespace UserServiceApi7.Interfaces;

public interface IRegisteredUserService : IService<DefaultRegisteredUserDto, CreateRegisteredUserDto> {
	Task<Result<DefaultRegisteredUserDto?>> GetRegisteredUserByEmailAsync(string email, CancellationToken ct);
	Task<Result<List<string>>> GetRolesOfUserAsync(int userId, string applicationKey, CancellationToken ct);
	Task<Result<DefaultRegisteredUserDto?>> AuthorizeAsync(LoginModel loginModel, CancellationToken ct);
	
	Task<Result<List<DefaultRegisteredUserDto>>> GetPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<Result<int>> CountPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	
	Task<Result<List<DefaultRegisteredUserDto>>> GetPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<Result<int>> CountPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
}