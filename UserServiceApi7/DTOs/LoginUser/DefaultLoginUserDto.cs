namespace UserServiceApi7.DTOs.LoginUser;

[CsvInfo("Loginuser")]
public class DefaultLoginUserDto {
	public int Id { get; set; }

	[Required]
	[CsvInfo("Email")]
	public string Email { get; set; } = null!;
	
	[Required]
	[CsvInfo("Vorname")]
	public string FirstName { get; set; } = null!;

	[Required]
	[CsvInfo("Nachname")]
	public string LastName { get; set; } = null!;
	
	[Required]
	public DateTime CreatedAt { get; set; }

	[JsonIgnore]
	public string FullName => $"{FirstName} {LastName}";
}