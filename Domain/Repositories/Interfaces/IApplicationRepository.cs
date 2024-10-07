namespace Domain.Repositories.Interfaces;

public interface IApplicationRepository : IRepository<Application> {
	Task<int> GetIdByApplicationKeyAsync(string applicationKey, CancellationToken ct);
}