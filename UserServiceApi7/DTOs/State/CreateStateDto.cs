namespace UserServiceApi7.DTOs.State;

public class CreateStateDto {
	public string Name { get; set; } = null!;
	public int CountryId { get; set; }
}