namespace UserServiceApi7.DTOs.Session;

public class CreateSessionDto {
	public string Token { get; set; } = null!;
	public DateTime CreatedAt { get; set; }
	public DateTime ValidUntil { get; set; }
	public int UserId { get; set; }
}