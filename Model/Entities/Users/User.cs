namespace Model.Entities.Users;

[Table("USERS")]
public class User {
	[Searchable]
	[Column("USER_ID")]
	public int Id { get; set; }

	public RegisteredUser RegisteredUser { get; set; } = null!;

	[Column("ADDRESS_ID")]
	public int AddressId { get; set; }

	public Address Address { get; set; } = null!;

	
	[Column("PRECEDING_TITLE")]
	public string? PrecedingTitle { get; set; }

	[Column("SUBSEQUENT_TITLE")]
	public string? SubsequentTitle { get; set; }

	[Column("GENDER_ID")]
	public int GenderId { get; set; }

	public Gender Gender { get; set; } = null!;

	[Column("BIRTH_DATE")]
	[Required]
	public DateOnly BirthDate { get; set; }

	[Column("BIRTH_PLACE")]
	public string? BirthPlace { get; set; }

	[Column("SALUTATION")]
	[Required]
	public string Salutation { get; set; } = null!;

	[Searchable]
	[Column("TELEPHONE")]
	[Required]
	public string Telephone { get; set; } = null!;

	[Column("NOTES")]
	public string? Notes { get; set; }
}