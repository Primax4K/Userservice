namespace UserServiceApi7.DTOs.RegisteredUserRole;

public class CreateRegisteredUserRoleDto {
	public int RegisteredUserId { get; set; }
	public int RoleId { get; set; }
	public int ApplicationId { get; set; }
}