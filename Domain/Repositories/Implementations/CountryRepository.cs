namespace Domain.Repositories.Implementations;

public class CountryRepository(UserServiceDbContext context) : ARepository<Country>(context), ICountryRepository {
	public async Task<List<Country>> SearchCountryAsync(string? searchString, CancellationToken ct) {
		if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
			return await Table
				.Take(20)
				.ToListAsync(ct);
		return await Table
			.AsSplitQuery()
			.Where(l => l.Name.Contains(searchString.Trim()))
			.Take(20)
			.ToListAsync(ct);
	}
}