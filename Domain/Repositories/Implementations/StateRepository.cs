namespace Domain.Repositories.Implementations;

public class StateRepository(UserServiceDbContext context) : ARepository<State>(context), IStateRepository {
	public async Task<List<State>> SearchStatesOfCountryAsync(string? query, int countryId, CancellationToken ct) {
		if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
			return await Table
				.Where(s => s.CountryId == countryId)
				.Take(20)
				.ToListAsync(ct);


		return await Table.Where(s => s.CountryId == countryId)
			.Where(s => s.Id.ToString().Contains(query) || s.Name.ToLower().Contains(query))
			.Take(20)
			.ToListAsync(ct);
	}
}