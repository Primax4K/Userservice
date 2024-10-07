namespace UserServiceApi7.DTOs.Address;

public class DefaultAddressDto {
	public int Id { get; set; }
	public int StateId { get; set; }
	public string Street { get; set; } = null!;
	public int ZipCode { get; set; }
	public string Location { get; set; } = null!;
	public string HouseNumber { get; set; } = null!;

	public override string ToString() => $"{Street} {HouseNumber}, {ZipCode} {Location}";
}