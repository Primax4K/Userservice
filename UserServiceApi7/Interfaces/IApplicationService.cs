namespace UserServiceApi7.Interfaces;

public interface IApplicationService : IService<DefaultApplicationDto, CreateApplicationDto> {
	Task<Result<int>> GetIdByApplicationKeyAsync(string applicationKey, CancellationToken ct);
}