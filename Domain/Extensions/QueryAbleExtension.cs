namespace Domain.Extensions;

public static class QueryableExtensions {
	public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string propertyName, bool ascending) {
		return ascending
			? source.OrderBy(e => EF.Property<TEntity>(e!, propertyName))
			: source.OrderByDescending(e => EF.Property<LoginUserData>(e!, propertyName));
	}
}