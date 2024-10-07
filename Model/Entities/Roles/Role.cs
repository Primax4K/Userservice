namespace Model.Entities.Roles;

[Table("ROLES")]
public class Role {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ROLE_ID")]
	[Searchable]
	public int Id { get; set; }

	[Searchable]
	[Column("ROLE_NAME")]
	[Required]
	public ERole Name { get; set; }
}