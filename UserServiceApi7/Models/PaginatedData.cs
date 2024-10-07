namespace UserServiceApi7.Models;

public class PaginatedData {
	public int Skip { get; set; }
	public int Take { get; set; }
	public bool Ascending { get; set; }
	public string SortPropertyLabel { get; set; } = null!;
	public string? Search { get; set; }
}