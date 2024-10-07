namespace UserServiceApi7.Interfaces;

public interface ISessionService : IService<DefaultSessionDto, CreateSessionDto> {
	Task<Result<int?>> GetUserIdByTokenAsync(string token, CancellationToken ct);
	Task<Result<bool>> IsValidSessionAsync(string token, int userId, CancellationToken ct);
	Task<Result<string?>> LoginAsync(LoginModel model, CancellationToken ct);
	Task<Result<bool>> IsValidSessionAsync(string token, CancellationToken ct);
	Task<Result<DefaultRegisteredUserDto>> ChangePasswordAsync(ChangePasswordModel model, CancellationToken ct);
	Task<Result<bool>> LogoutEverywhereAsync(int userId, CancellationToken ct);
}