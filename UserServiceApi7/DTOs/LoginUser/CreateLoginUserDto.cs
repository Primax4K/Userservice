namespace UserServiceApi7.DTOs.LoginUser;

public class CreateLoginUserDto {
	[EmailAddress]
	[Required]
	public string Email { get; set; } = null!;

	[Required]
	public string FirstName { get; set; } = null!;

	[Required]
	public string LastName { get; set; } = null!;

	public DateTime CreatedAt { get; set; } = DateTime.Now;
}