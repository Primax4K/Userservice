namespace UserServiceApi7.DTOs.Gender;

public class DefaultGenderDto {
	public int Id { get; set; }
	public string GenderName { get; set; } = null!;

	public override string ToString() => GenderName;
}