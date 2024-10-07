namespace Model.Entities.Roles;

[Table("APPLICATIONS_HAS_ROLES_JT")]
public class ApplicationRole {
	[Column("ROLE_ID")]
	public int RoleId { get; set; }

	public Role Role { get; set; } = null!;

	[Column("APPLICATION_ID")]
	public int ApplicationId { get; set; }

	public Application Application { get; set; } = null!;
}