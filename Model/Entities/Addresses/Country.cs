namespace Model.Entities.Addresses;

[Table("COUNTRIES")]
public class Country {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("COUNTRY_ID")]
	[Searchable]
	public int Id { get; set; }

	[Searchable]
	[Column("NAME")]
	[Required]
	public string Name { get; set; } = null!;
}