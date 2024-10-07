namespace UserServiceApi7.Interfaces;

public interface IService<TDefaultDto, TCreateDto> {
	Task<Result<TDefaultDto>> ReadAsync(int id, CancellationToken ct);
	Task<Result<TDefaultDto>> CreateAsync(TCreateDto createEntityDto, CancellationToken ct);
	Task<Result<bool>> UpdateAsync(int id, TDefaultDto defaultEntityDto, CancellationToken ct);
	Task<Result<bool>> DeleteAsync(int id, CancellationToken ct);
	Task<Result<List<TDefaultDto>>> SearchAsync(string?  query, CancellationToken ct);
}