namespace Domain.Repositories.Interfaces;

public interface IRegisteredUserRepository : IRepository<RegisteredUser> {
    Task<RegisteredUser?> GetByEmailAsync(string email, CancellationToken ct);
    Task<RegisteredUser?> AuthorizeAsync(int id, CancellationToken ct);
    Task<RegisteredUser?> AuthorizeAsync(LoginModel model, CancellationToken ct);
    Task<List<string>> GetRolesOfUserAsync(int id, string applicationKey, CancellationToken ct);
    Task<List<ERole>> GetEnumRolesOfUserAsync(int id, string applicationKey, CancellationToken ct);
    Task<RegisteredUser?> FindWithoutTrackingAsync(int id, CancellationToken ct);
    
    Task<List<RegisteredUser>> GetPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
    Task<int> CountPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
    Task<List<RegisteredUser>> GetPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
    Task<int> CountPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct);
}