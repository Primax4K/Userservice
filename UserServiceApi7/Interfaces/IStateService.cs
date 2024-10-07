namespace UserServiceApi7.Interfaces;

public interface IStateService : IService<DefaultStateDto, CreateStateDto> {
	Task<Result<List<DefaultStateDto>>> GetStatesOfCountryAsync(int countryId, string? query, CancellationToken ct);
}