namespace UserServiceApi7.DTOs.RegisteredUser;

public class DefaultRegisteredUserDto {
	public int Id { get; set; }
	
	[Required]
	public bool IsLocked { get; set; }
}