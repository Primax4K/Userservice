namespace Model.Entities.Users;

[Table("LOGIN_USER_DATA")]
public class LoginUserData {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("USER_ID")]
	[Searchable]
	public int Id { get; set; }

	[Searchable]
	[Column("EMAIL")]
	[Required]
	public string Email { get; set; } = null!;

	[Searchable]
	[Column("FIRST_NAME")]
	[Required]
	public string FirstName { get; set; } = null!;

	[Searchable]
	[Column("LAST_NAME")]
	[Required]
	public string LastName { get; set; } = null!;

	[Column("CREATED_AT")]
	[Required]
	public DateTime CreatedAt { get; set; }
}