namespace Domain.Repositories.Interfaces;

public interface IUserRepository : IRepository<User> {
	Task<List<User>> GetPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct);
	Task<int> CountPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct);
}