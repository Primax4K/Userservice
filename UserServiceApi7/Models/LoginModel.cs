namespace UserServiceApi7.Models;

public class LoginModel {
	public LoginModel() { }

	public LoginModel(string email, string password) {
		Email = email;
		Password = password;
	}

	[Required(ErrorMessage = "Die Email ist erforderlich.")]
	[EmailAddress(ErrorMessage = "Ungültige Email")]
	public string Email { get; set; } = null!;

	[Required(ErrorMessage = "Das Passwort ist erforderlich.")]
	[MinLength(6, ErrorMessage = "Passwort muss mindestens 6 Zeichen lang sein.")]
	public string Password { get; set; } = null!;
}