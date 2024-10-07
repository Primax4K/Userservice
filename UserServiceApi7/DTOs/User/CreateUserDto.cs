namespace UserServiceApi7.DTOs.User;

public class CreateUserDto {
	public int Id { get; set; }
	public int AddressId { get; set; }
	public string? PrecedingTitle { get; set; }
	public string? SubsequentTitle { get; set; }
	public int GenderId { get; set; }

	[Required]
	public DateOnly BirthDate { get; set; }

	public string? BirthPlace { get; set; }

	[Required]
	public string Salutation { get; set; } = null!;

	[Required]
	public string Telephone { get; set; } = null!;

	public string? Notes { get; set; }
}