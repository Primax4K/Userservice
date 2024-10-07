namespace Model.Entities.Addresses;

[Table("ADDRESSES")]
public class Address {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ADDRESS_ID")]
	[Searchable]
	public int Id { get; set; }

	[Column("STATE_ID")]
	public int StateId { get; set; }

	public State State { get; set; } = null!;

	[Searchable]
	[Column("STREET")]
	[Required]
	public string Street { get; set; } = null!;

	[Searchable]
	[Column("ZIP_CODE")]
	[Required]
	public int ZipCode { get; set; }

	[Searchable]
	[Column("LOCATION")]
	[Required]
	public string Location { get; set; } = null!;

	[Searchable]
	[Column("HOUSE_NUMBER")]
	[Required]
	public string HouseNumber { get; set; } = null!;
}