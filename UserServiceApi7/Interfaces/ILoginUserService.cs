namespace UserServiceApi7.Interfaces;

public interface ILoginUserService : IService<DefaultLoginUserDto, CreateLoginUserDto> {
	Task<Result<DefaultLoginUserDto?>> GetLoginUserByEmail(string email, CancellationToken ct);
	Task<Result<List<DefaultLoginUserDto>>> GetPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<Result<int>> CountPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<Result<DefaultFullUserDto>> GetFullUserAsync(int userId, string applicationKey, CancellationToken ct);
	Task<Result<List<DefaultLoginUserDto>>> ReadRangeAsync(List<int> ids, CancellationToken ct);
	Task<Result<List<DefaultLoginUserDto>>> GetExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct);
	Task<Result<int>> CountExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct);
	Task<Result<List<DefaultLoginUserDto>>> GetPaginatedUsersInList(List<int> userIds, PaginatedData paginatedData, CancellationToken ct);

}