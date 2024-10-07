namespace UserServiceApi7.DTOs.LoginAttempt;

public class DefaultLoginAttemptDto {
	public int Id { get; set; }
	public DateTime AttemptedDate { get; set; }
	public string IpAddress { get; set; } = null!;
	public int RegisteredUserId { get; set; }
	public string AttemptStatus { get; set; } = null!;
	public int ApplicationId { get; set; }
}