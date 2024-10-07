namespace Model.Entities.Users;

[Table("GENDERS")]
public class Gender {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("GENDER_ID")]
	[Searchable]
	public int Id { get; set; }

	[Searchable]
	[Column("GENDER_NAME")]
	[Required]
	public string GenderName { get; set; } = null!;
}