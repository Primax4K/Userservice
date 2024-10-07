namespace Domain.Repositories.Implementations;

public class SessionRepository(UserServiceDbContext context) : ARepository<Session>(context), ISessionRepository {
    public async Task<bool> IsValidSessionAsync(string token, int userId, CancellationToken ct) {
        var now = DateTime.Now; //this variable is necessary because EF Core does not support DateTime.Now in this query
        return await Table.AnyAsync(s => s.Token == token && s.UserId == userId && s.ValidUntil > now, ct);
    }

    public async Task<int?> GetUserIdByTokenAsync(string token, CancellationToken ct) {
        return await Table.Where(s => s.Token == token).Select(s => s.UserId).FirstOrDefaultAsync(ct);
    }

    public async Task<bool> IsValidSessionAsync(string token, CancellationToken ct) {
        var now = DateTime.Now;
        return await Table.AnyAsync(s => s.Token == token && s.ValidUntil > now, ct);
    }
}