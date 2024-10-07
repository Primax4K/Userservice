namespace Model.Entities.Users;

[Table("REGISTERED_USERS")]
public class RegisteredUser {
	[Searchable]
	[Column("REGISTERED_USER_ID")]
	public int Id { get; set; }

	public LoginUserData LoginUser { get; set; } = null!;

	[Column("PASSWORD_HASH")]
	[Required]
	public string PasswordHash { get; set; }

	[Column("IS_LOCKED")]
	[Required]
	public bool IsLocked { get; set; }

	public RegisteredUser ClearSensitiveData() {
		PasswordHash = null!;
		return this;
	}

	[NotMapped]
	public List<RegisteredUserRole> RegisteredUserRoles { get; set; } = new();

	public bool HasRole(ERole role) {
		return RegisteredUserRoles.Any(r => r.ApplicationRole.Role.Name == role);
	}

	public static bool VerifyPassword(string plainPassword, string hashedPassword) {
		return BC.Verify(plainPassword, hashedPassword);
	}
}