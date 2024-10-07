namespace UserServiceApi7.Providers;

public class ApiProvider {
	private string? _token;

	public string? Token {
		get => _token;
		set {
			_token = value;
			TokenChanged?.Invoke();
		}
	}

	public event Action? TokenChanged;
}