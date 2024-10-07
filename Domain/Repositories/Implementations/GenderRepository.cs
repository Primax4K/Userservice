namespace Domain.Repositories.Implementations;

public class GenderRepository(UserServiceDbContext context) : ARepository<Gender>(context), IGenderRepository {
	public async Task<List<Gender>> ReadAllAsync(CancellationToken ct) {
		return await Table.ToListAsync(ct);
	}
}