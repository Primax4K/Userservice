namespace Domain.Repositories.Interfaces;

public interface ICountryRepository : IRepository<Country> {
	Task<List<Country>> SearchCountryAsync(string? searchString, CancellationToken ct);
}