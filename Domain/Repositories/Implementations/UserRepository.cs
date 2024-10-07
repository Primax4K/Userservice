namespace Domain.Repositories.Implementations;

public class UserRepository(UserServiceDbContext context) : ARepository<User>(context), IUserRepository {
	public async Task<List<User>> GetPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		var properties = typeof(User).GetProperties();

		var sortingLabel = string.Empty;
		foreach (var property in properties) {
			if (!string.Equals(property.Name, paginatedData.SortPropertyLabel,
				    StringComparison.CurrentCultureIgnoreCase)) continue;
			sortingLabel = property.Name;
			break;
		}
		
		if (string.IsNullOrEmpty(sortingLabel)) {
			sortingLabel = "Id";
		}

		List<User> users;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			users = await Table.IgnoreAutoIncludes()
				.Include(u => u.RegisteredUser)
				.Where(u => u.RegisteredUser.IsLocked == false)
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			users = await Table.IgnoreAutoIncludes().Include(u => u.RegisteredUser).Include(u => u.RegisteredUser.LoginUser)
				.Where(u => u.RegisteredUser.IsLocked == false)
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.RegisteredUser.LoginUser.Email.ToLower().Contains(search) || c.RegisteredUser.LoginUser.FirstName.ToLower().Contains(search) || c.RegisteredUser.LoginUser.LastName.ToLower().Contains(search))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}

		return users;
	}

	public async Task<int> CountPaginatedUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		var properties = typeof(User).GetProperties();

		var sortingLabel = string.Empty;
		foreach (var property in properties) {
			if (!string.Equals(property.Name, paginatedData.SortPropertyLabel,
				    StringComparison.CurrentCultureIgnoreCase)) continue;
			sortingLabel = property.Name;
			break;
		}
		
		if (string.IsNullOrEmpty(sortingLabel)) {
			sortingLabel = "Id";
		}

		int users;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			users = await Table.IgnoreAutoIncludes()
				.Include(u => u.RegisteredUser)
				.Where(u => u.RegisteredUser.IsLocked == false)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.CountAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			users = await Table.IgnoreAutoIncludes().Include(u => u.RegisteredUser).Include(u => u.RegisteredUser.LoginUser)
				.Include(u => u.RegisteredUser)
				.Where(u => u.RegisteredUser.IsLocked == false)
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.RegisteredUser.LoginUser.Email.ToLower().Contains(search) || c.RegisteredUser.LoginUser.FirstName.ToLower().Contains(search) || c.RegisteredUser.LoginUser.LastName.ToLower().Contains(search))
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.CountAsync(ct);
		}

		return users;
	}
}