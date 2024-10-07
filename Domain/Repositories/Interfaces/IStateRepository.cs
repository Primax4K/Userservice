namespace Domain.Repositories.Interfaces;

public interface IStateRepository : IRepository<State> {
	Task<List<State>> SearchStatesOfCountryAsync(string? query, int countryId, CancellationToken ct );
}