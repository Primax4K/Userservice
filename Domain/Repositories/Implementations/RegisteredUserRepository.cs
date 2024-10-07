namespace Domain.Repositories.Implementations;

public class RegisteredUserRepository(UserServiceDbContext context, ILoginUserRepository loginUserRepository) :
	ARepository<RegisteredUser>(context), IRegisteredUserRepository {
	public async Task<RegisteredUser?> GetByEmailAsync(string email, CancellationToken ct) {
		var loginUser = await loginUserRepository.GetByEmailAsync(email, ct);
		if (loginUser is null) return null;
		return await ReadAsync(loginUser.Id, ct);
	}

	public async Task<RegisteredUser?> AuthorizeAsync(int id, CancellationToken ct) {
		var user = await FindWithoutTrackingAsync(id, ct);

		return user?.ClearSensitiveData();
	}

	public async Task<RegisteredUser?> AuthorizeAsync(LoginModel model, CancellationToken ct) {
		var loginUser = await loginUserRepository.GetByEmailAsync(model.Email, ct);

		if (loginUser is null)
			return null;

		var regUser = await FindWithoutTrackingAsync(loginUser.Id, ct);

		if (regUser is null) return null;

		if (!RegisteredUser.VerifyPassword(model.Password, regUser.PasswordHash)) return null!;

		return regUser.ClearSensitiveData();
	}

	public async Task<List<string>> GetRolesOfUserAsync(int id, string applicationKey, CancellationToken ct) {
		var roles = await Table
			.Where(x => x.Id == id)
			.SelectMany(x => x.RegisteredUserRoles)
			.Where(x => x.ApplicationRole.Application.Key == applicationKey)
			.Select(x => x.ApplicationRole.Role.Name.ToEnumString())
			.ToListAsync(cancellationToken: ct);

		return roles;
	}

	public async Task<List<ERole>> GetEnumRolesOfUserAsync(int id, string applicationKey, CancellationToken ct) {
		var roles = await Table
			.Where(x => x.Id == id)
			.SelectMany(x => x.RegisteredUserRoles)
			.Where(x => x.ApplicationRole.Application.Key == applicationKey)
			.Select(x => x.ApplicationRole.Role.Name)
			.ToListAsync(cancellationToken: ct);

		return roles;
	}

	public async Task<RegisteredUser?> FindWithoutTrackingAsync(int id, CancellationToken ct) {
		return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
	}

	public async Task<List<RegisteredUser>> GetPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		var properties = typeof(RegisteredUser).GetProperties();

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

		List<RegisteredUser> registeredUsers;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			registeredUsers = await Table.IgnoreAutoIncludes()
				.Where(r => r.IsLocked == false)
				.Where(registeredUser => !Context.Users.Any(fullUser => fullUser.Id == registeredUser.Id))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			registeredUsers = await Table.IgnoreAutoIncludes().Include(r => r.LoginUser)
				.Where(r => r.IsLocked == false)
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.LoginUser.Email.ToLower().Contains(search) || c.LoginUser.FirstName.ToLower().Contains(search) || c.LoginUser.LastName.ToLower().Contains(search))
				.Where(registeredUser => !Context.Users.Any(fullUser => fullUser.Id == registeredUser.Id))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}

		return registeredUsers;
	}

	public async Task<int> CountPaginatedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		var properties = typeof(LoginUserData).GetProperties();

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

		int registeredUsers;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			registeredUsers = await Table.IgnoreAutoIncludes()
				.Where(r => r.IsLocked == false)
				.Where(registeredUser => !Context.Users.Any(fullUser => fullUser.Id == registeredUser.Id))
				.CountAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			registeredUsers = await Table.IgnoreAutoIncludes().Include(r => r.LoginUser)
				.Where(r => r.IsLocked == false)
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.LoginUser.Email.ToLower().Contains(search) || c.LoginUser.FirstName.ToLower().Contains(search) || c.LoginUser.LastName.ToLower().Contains(search))
				.Where(registeredUser => !Context.Users.Any(fullUser => fullUser.Id == registeredUser.Id))
				.CountAsync(ct);
		}

		return registeredUsers;
	}

	public async Task<List<RegisteredUser>> GetPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		var properties = typeof(RegisteredUser).GetProperties();

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

		List<RegisteredUser> registeredUsers;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			registeredUsers = await Table.IgnoreAutoIncludes()
				.Where(r => r.IsLocked)
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			registeredUsers = await Table.IgnoreAutoIncludes().Include(r => r.LoginUser)
				.Where(r => r.IsLocked)
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.LoginUser.Email.ToLower().Contains(search) || c.LoginUser.FirstName.ToLower().Contains(search) || c.LoginUser.LastName.ToLower().Contains(search))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.Where(registeredUser => !Context.Users.Any(fullUser => fullUser.Id == registeredUser.Id))
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}

		return registeredUsers;
	}

	public async Task<int> CountPaginatedLockedRegisteredUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
		var properties = typeof(RegisteredUser).GetProperties();

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

		int registeredUsersCount;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			registeredUsersCount = await Table.IgnoreAutoIncludes()
				.Where(r => r.IsLocked)
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.CountAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			registeredUsersCount = await Table.IgnoreAutoIncludes().Include(r => r.LoginUser)
				.Where(r => r.IsLocked)
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.LoginUser.Email.ToLower().Contains(search) || c.LoginUser.FirstName.ToLower().Contains(search) || c.LoginUser.LastName.ToLower().Contains(search))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.Where(registeredUser => !Context.Users.Any(fullUser => fullUser.Id == registeredUser.Id))
				.CountAsync(ct);
		}

		return registeredUsersCount;
	}
}