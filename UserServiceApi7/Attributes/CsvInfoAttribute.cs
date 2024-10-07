namespace UserServiceApi7.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class CsvInfoAttribute : Attribute {
	public CsvInfoAttribute(string title) {
		Title = title;
	}

	public string Title { get; init; }
}