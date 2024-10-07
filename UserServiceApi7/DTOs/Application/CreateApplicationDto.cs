namespace UserServiceApi7.DTOs.Application;

public class CreateApplicationDto {
	public string Name { get; set; } = null!;
	public string Url { get; set; } = null!;
	public int Port { get; set; }
	public string Key { get; set; } = null!;
}