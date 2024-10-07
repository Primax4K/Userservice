namespace Domain.Repositories.Implementations;

public class LoginUserRepository(UserServiceDbContext context, IServiceProvider serviceProvider, IRegisteredUserRoleRepository registeredUserRoleRepository) : ARepository<LoginUserData>(context),
	ILoginUserRepository {
	public async Task<LoginUserData?> GetByEmailAsync(string email, CancellationToken ct) {
		return await Table.FirstOrDefaultAsync(l => l.Email == email.ToLower().Trim(), ct);
	}

	public async Task<List<LoginUserData>> GetPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
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

		List<LoginUserData> loginUsers;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			loginUsers = await Table
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			loginUsers = await Table
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.Email.ToLower().Contains(search) || c.FirstName.ToLower().Contains(search) || c.LastName.ToLower().Contains(search))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}

		return loginUsers;
	}

	public async Task<int> CountPaginatedLoginUsersAsync(PaginatedData paginatedData, CancellationToken ct) {
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

		int loginUserCount;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			loginUserCount = await Table
				.CountAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			loginUserCount = await Table
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.Email.ToLower().Contains(search) || c.FirstName.ToLower().Contains(search) || c.LastName.ToLower().Contains(search))
				.CountAsync(ct);
		}

		return loginUserCount;
	}

	public async Task<DefaultFullUserDto> GetFullUserAsync(int userId, string applicationKey, CancellationToken ct) {
		var loginUser = await Table.FirstOrDefaultAsync(l => l.Id == userId, ct);

		//otherwise circular reference
		var registeredUserRepository = serviceProvider.GetRequiredService<IRegisteredUserRepository>();

		var registeredUser = await registeredUserRepository.FirstOrDefaultAsync(r => r.Id == userId, ct);

		var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);

		Address? address = null;
		if (user is not null) {
			address = await Context.Addresses.FirstOrDefaultAsync(a => a.Id == user!.AddressId, ct);
		}

		var role = (await registeredUserRepository.GetEnumRolesOfUserAsync(userId, applicationKey, ct)).FirstOrDefault();

		return new DefaultFullUserDto {
			LoginUser = loginUser?.Adapt<DefaultLoginUserDto>(),
			RegisteredUser = registeredUser?.Adapt<DefaultRegisteredUserDto>(),
			User = user?.Adapt<DefaultUserDto>(),
			Role = role,
			Address = address?.Adapt<DefaultAddressDto>()
		};
	}

	public async Task<List<LoginUserData>> ReadRangeAsync(List<int> ids, CancellationToken ct) {
		//otherwise the ids will get sorted
		var unorderedResults = await Table.Where(l => ids.Contains(l.Id)).ToListAsync(ct);
		var orderedResults = ids.Join(
			unorderedResults,
			id => id,
			user => user.Id,
			(id, user) => user
		).ToList();

		return orderedResults;
	}

	public async Task<List<LoginUserData>> GetExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct) {
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

		List<LoginUserData> loginUsers;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			loginUsers = await Table
				.Where(l => !userIds.Contains(l.Id))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			loginUsers = await Table
				.Where(l => !userIds.Contains(l.Id))
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.Email.ToLower().Contains(search) || c.FirstName.ToLower().Contains(search) || c.LastName.ToLower().Contains(search))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}

		return loginUsers;
	}

	public async Task<int> CountExceptedUsersPaginatedAsync(List<int> userIds, PaginatedData paginatedData, CancellationToken ct) {
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

		int loginUserCount;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			loginUserCount = await Table
				.Where(l => !userIds.Contains(l.Id))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.CountAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			loginUserCount = await Table
				.Where(l => !userIds.Contains(l.Id))
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.Email.ToLower().Contains(search) || c.FirstName.ToLower().Contains(search) || c.LastName.ToLower().Contains(search))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.CountAsync(ct);
		}

		return loginUserCount;
	}

	public async Task<List<LoginUserData>> GetPaginatedUsersInList(List<int> userIds, PaginatedData paginatedData, CancellationToken ct) {
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

		List<LoginUserData> loginUsers;

		if (string.IsNullOrEmpty(paginatedData.Search)) {
			loginUsers = await Table
				.Where(l => userIds.Contains(l.Id))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}
		else {
			var search = paginatedData.Search.ToLower();
			loginUsers = await Table
				.Where(l => userIds.Contains(l.Id))
				.Where(c => c.Id.ToString().ToLower().Contains(search) || c.Email.ToLower().Contains(search) || c.FirstName.ToLower().Contains(search) || c.LastName.ToLower().Contains(search))
				.Skip(paginatedData.Skip)
				.Take(paginatedData.Take)
				.OrderBy(sortingLabel, paginatedData.Ascending)
				.ToListAsync(ct);
		}

		return loginUsers;
	}
}