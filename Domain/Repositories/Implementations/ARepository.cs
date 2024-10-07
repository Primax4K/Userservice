namespace Domain.Repositories.Implementations;

public abstract class ARepository<TEntity>(UserServiceDbContext context) : IRepository<TEntity>
	where TEntity : class {
	protected readonly UserServiceDbContext Context = context;
	protected readonly DbSet<TEntity> Table = context.Set<TEntity>();

	public virtual async Task<List<TEntity>> ReadAsync(CancellationToken ct) {
		return await Table.ToListAsync(ct);
	}

	public virtual async Task<TEntity?> ReadAsync(int id, CancellationToken ct) {
		return await Table.FindAsync(new object[] { id }, ct);
	}

	public virtual async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
		return await Table.Where(filter).ToListAsync(ct);
	}

	public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
		return await Table.FirstOrDefaultAsync(filter, ct);
	}

	public async Task<bool> ExistsAsync(int id, CancellationToken ct) {
		return await Table.FindAsync(new object[] { id }, ct) != null;
	}

	public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
		return await Table.IgnoreAutoIncludes().AnyAsync(filter, ct);
	}

	public async Task<List<TEntity>> SearchAsync(string? query, CancellationToken ct) {
		if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
			return await Table
				.Take(20)
				.ToListAsync(ct);


		var propertyInfos = typeof(TEntity).GetProperties().Where(
			prop => Attribute.IsDefined(prop, typeof(SearchableAttribute))).ToList();

		if (propertyInfos.Count == 0)
			return [];


		var parameter = Expression.Parameter(typeof(TEntity), "entity");
		Expression? filter = null;

		var dbFunctions = EF.Functions;

		foreach (var prop in propertyInfos) {
			var property = Expression.Property(parameter, prop.Name);
			var value = Expression.Constant($"%{query.Trim()}%");
			var likeMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), [typeof(DbFunctions), typeof(string), typeof(string)]);

			if (prop.PropertyType == typeof(string)) {
				var like = Expression.Call(likeMethod!, Expression.Constant(dbFunctions), property, value);
				filter = filter == null ? like : Expression.OrElse(filter, like);
			}
			else if (prop.PropertyType == typeof(int)) {
				if (!int.TryParse(query, out var intValue)) continue;
				var equalsMethod = typeof(int).GetMethod("Equals", new[] { typeof(int) });
				var equals = Expression.Call(property, equalsMethod!, Expression.Constant(intValue, typeof(int)));
				filter = filter == null ? equals : Expression.OrElse(filter, equals);
			}
		}

		var lambda = Expression.Lambda<Func<TEntity, bool>>(filter!, parameter);

		var filteredItems = await Table.Where(lambda).Take(20).ToListAsync(ct);

		return filteredItems;
	}

	public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct) {
		Table.Add(entity);
		await Context.SaveChangesAsync(ct);
		return entity;
	}

	public virtual async Task<List<TEntity>> CreateAsync(List<TEntity> entity, CancellationToken ct) {
		Table.AddRange(entity);
		await Context.SaveChangesAsync(ct);
		return entity;
	}

	public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct) {
		Context.ChangeTracker.Clear();
		Table.Update(entity);
		await Context.SaveChangesAsync(ct);
	}

	public virtual async Task UpdateAsync(IEnumerable<TEntity> entity, CancellationToken ct) {
		Context.ChangeTracker.Clear();
		Table.UpdateRange(entity);
		await Context.SaveChangesAsync(ct);
	}

	public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct) {
		Context.ChangeTracker.Clear();
		Table.Remove(entity);
		await Context.SaveChangesAsync(ct);
	}

	public virtual async Task DeleteAsync(IEnumerable<TEntity> entity, CancellationToken ct) {
		Context.ChangeTracker.Clear();
		Table.RemoveRange(entity);
		await Context.SaveChangesAsync(ct);
	}

	public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
		Context.ChangeTracker.Clear();
		Table.RemoveRange(Table.Where(filter));
		await Context.SaveChangesAsync(ct);
	}
}