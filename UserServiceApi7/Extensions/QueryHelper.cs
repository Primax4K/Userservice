namespace UserServiceApi7.Extensions;

public static class QueryHelper {
	public static string ToQueryString(this PaginatedData model) {
		if (model == null) {
			Console.WriteLine("model is null");
			throw new ArgumentNullException(nameof(model));
		}

		var properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
			.Where(prop => prop.CanRead && prop.GetIndexParameters().Length == 0);

		var queryString = new StringBuilder();

		foreach (var property in properties) {
			var value = property.GetValue(model);

			if (value == null) continue;
			if (queryString.Length > 0)
				queryString.Append('&');

			queryString.AppendFormat("{0}={1}",
				Uri.EscapeDataString(property.Name),
				Uri.EscapeDataString(value.ToString()!));
		}

		return queryString.ToString();
	}

	public static string ToQueryList<TKey>(this IEnumerable<TKey> list, string listName) where TKey : struct {
		if (list == null) {
			Console.WriteLine("list is null");
			throw new ArgumentNullException(nameof(list));
		}

		var queryString = new StringBuilder();

		foreach (var item in list) {
			if (queryString.Length > 0)
				queryString.Append('&');

			queryString.AppendFormat("{0}={1}",
				Uri.EscapeDataString(listName),
				Uri.EscapeDataString(item.ToString()!));
		}


		return queryString.ToString();
	}
}