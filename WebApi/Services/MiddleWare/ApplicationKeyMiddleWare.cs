namespace WebApi.Services.MiddleWare;

public class ApplicationKeyMiddleWare(RequestDelegate next) {
	public async Task InvokeAsync(HttpContext context, UserServiceDbContext dbContext) {
		var apiKey = context.Request.Headers["ApplicationKey"].FirstOrDefault()?.Split(" ").Last();

		if (context.Request.IsHttps) {
			foreach (var header in context.Request.Headers) {
				Console.WriteLine($"{header.Key}: {header.Value}");
			}

			if (context.Request.Headers.Count == 0) Console.WriteLine("No Headers");
		}

		var isValidApiKey = await dbContext.Applications.AnyAsync(key => key.Key == apiKey);
		if (!isValidApiKey) {
			Console.WriteLine("Invalid API Key: " + apiKey);
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync("Invalid API Key");

			return;
		}

		await next(context);
	}
}