namespace Domain.Repositories.Interfaces;

public interface ILoginUserRepository : IRepository<LoginUserData> {
	Task<LoginUserData?> GetByEmailAsync(string email, CancellationToken ct);
	Task<List<LoginUserData>> GetPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<int> CountPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<DefaultFullUserDto> GetFullUserAsync(int userId, string applicationKey, CancellationToken ct);
	Task<List<LoginUserData>> ReadRangeAsync(List<int> ids, CancellationToken ct);
	Task<List<LoginUserData>> GetExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct);
	Task<int> CountExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct);
	Task<List<LoginUserData>> GetPaginatedUsersInList(List<int> userIds, PaginatedData paginatedData, CancellationToken ct);
}