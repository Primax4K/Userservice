namespace UserServiceApi7.Models;

public class ChangePasswordModel {
	public DefaultRegisteredUserDto RegisteredUser { get; set; } = null!;
	public string NewPasswordHash { get; set; } = null!;
}