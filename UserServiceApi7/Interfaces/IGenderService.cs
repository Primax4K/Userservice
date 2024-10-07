namespace UserServiceApi7.Interfaces;

public interface IGenderService : IService<DefaultGenderDto, CreateGenderDto> {
	Task<Result<List<DefaultGenderDto>>> ReadAllAsync(CancellationToken ct);
}