namespace WebApi.Services;

public class CustomHeader : IOperationFilter {
	public void Apply(OpenApiOperation operation, OperationFilterContext context) {
		operation.Parameters ??= new List<OpenApiParameter>();

		var authToken = new OpenApiParameter {
			Name = "Auth",
			In = ParameterLocation.Header,
			Description = "Auth token",
			Schema = new OpenApiSchema {
				Type = "CustomAuth"
			},
			Style = ParameterStyle.Simple,
			Required = false
		};
		operation.Parameters.Add(authToken);

		var applicationKey = new OpenApiParameter {
			Name = "ApplicationKey",
			In = ParameterLocation.Header,
			Description = "Application key",
			Schema = new OpenApiSchema {
				Type = "Custom"
			},
			Style = ParameterStyle.Simple,
			Required = false
		};
		operation.Parameters.Add(applicationKey);
	}
}