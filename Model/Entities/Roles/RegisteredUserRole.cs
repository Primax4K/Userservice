namespace Model.Entities.Roles;

[Table("ROLES_has_REGISTERED_USERS")]
public class RegisteredUserRole {
	[Column("REGISTERED_USER_ID")]
	public int RegisteredUserId { get; set; }

	public RegisteredUser RegisteredUser { get; set; } = null!;

	[Column("ROLE_ID")]
	public int RoleId { get; set; }

	[Column("APPLICATION_ID")]
	public int ApplicationId { get; set; }

	public ApplicationRole ApplicationRole { get; set; } = null!;
}