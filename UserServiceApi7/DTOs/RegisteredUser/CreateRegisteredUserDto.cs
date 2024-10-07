namespace UserServiceApi7.DTOs.RegisteredUser;

public class CreateRegisteredUserDto {
	public string Email { get; set; } = null!;
	public string PasswordHash { get; set; } = null!;
	public bool IsLocked { get; set; }
	public ERole Role { get; set; }
}