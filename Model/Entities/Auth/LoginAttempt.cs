namespace Model.Entities.Auth;

[Table("LOGIN_ATTEMPTS")]
public class LoginAttempt {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ATTEMPT_ID")]
	[Searchable]
	public int Id { get; set; }

	[Column("ATTEMPTED_DATE")]
	[Required]
	public DateTime AttemptedDate { get; set; }

	[Searchable]
	[Column("IP_ADDRESS")]
	[Required]
	public string IpAddress { get; set; } = null!;

	[Column("REGISTERED_USER_ID")]
	public int RegisteredUserId { get; set; }

	public RegisteredUser RegisteredUser { get; set; } = null!;

	[Searchable]
	[Column("ATTEMPT_STATUS")]
	[Required]
	public string AttemptStatus { get; set; } = null!;

	[Column("APPLICATION_ID")]
	public int ApplicationId { get; set; }

	public Application Application { get; set; } = null!;
}