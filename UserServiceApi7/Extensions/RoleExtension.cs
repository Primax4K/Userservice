namespace UserServiceApi7.Extensions;

public static class RoleExtension {
	public static string ToEnumString(this ERole role) => role switch {
		ERole.Patient => "Patient",
		ERole.Assistant => "Assistant",
		ERole.Taxconsultant => "Taxconsultant",
		ERole.Administrator => "Administrator",
		_ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
	};
}