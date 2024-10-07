namespace Model.Entities.Applications;

[Table("APPLICATIONS")]
public class Application {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("APPLICATION_ID")]
	[Searchable]
	public int Id { get; set; }

	[Searchable]
	[Column("APPLICATION_NAME")]
	[Required]
	public string Name { get; set; } = null!;

	[Searchable]
	[Column("APPLICATION_URL")]
	[Required]
	public string Url { get; set; } = null!;

	[Searchable]
	[Column("PORT")]
	[Required]
	public int Port { get; set; }

	[Column("APPLICATION_KEY")]
	[Required]
	public string Key { get; set; } = null!;
}