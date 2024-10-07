namespace Domain.Repositories.Implementations;

public class ApplicationRepository(UserServiceDbContext context) : ARepository<Application>(context), IApplicationRepository {
	public async Task<int> GetIdByApplicationKeyAsync(string applicationKey, CancellationToken ct) {
		return await Table.Where(a => a.Key == applicationKey).Select(a => a.Id).FirstAsync(ct);
	}
}