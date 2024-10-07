namespace UserServiceApi7.DTOs.State;

public class DefaultStateDto {
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public int CountryId { get; set; }

	public override string ToString() => Name;
}