namespace Domain.Repositories.Interfaces;

public interface IGenderRepository : IRepository<Gender> {
	Task<List<Gender>> ReadAllAsync(CancellationToken ct);
}