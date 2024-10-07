namespace Model.Entities.Addresses;

[Table("STATES")]
public class State {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("STATE_ID")]
	[Searchable]
	public int Id { get; set; }

	[Searchable]
	[Column("NAME")]
	[Required]
	public string Name { get; set; } = null!;

	[Column("COUNTRY_ID")]
	public int CountryId { get; set; }

	public Country Country { get; set; } = null!;
}