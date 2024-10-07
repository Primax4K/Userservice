namespace Model.Entities.Auth;

[Table("SESSIONS")]
public class Session {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("SESSION_ID")]
	[Searchable]
	public int Id { get; set; }

	[Searchable]
	[Column("SESSION_TOKEN")]
	[Required]
	public string Token { get; set; } = null!;

	[Column("CREATED_AT")]
	[Required]
	public DateTime CreatedAt { get; set; }

	[Column("VALID_UNTIL")]
	[Required]
	public DateTime ValidUntil { get; set; }

	[Column("USER_ID")]
	[Required]
	public int UserId { get; set; }

	public RegisteredUser RegisteredUser { get; set; } = null!;
}