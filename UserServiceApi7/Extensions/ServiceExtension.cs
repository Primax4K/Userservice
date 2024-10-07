namespace UserServiceApi7.Extensions;

public static class ServiceExtension {
	/// <summary>
	/// Registers the services of the UserServiceAPI.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="url"></param>
	/// <param name="applicationKey"></param>
	/// <returns></returns>
	public static IServiceCollection AddUserServiceApi(this IServiceCollection services, string url, string applicationKey) {
		services.AddScoped<ApiProvider>();
		
		Globals.UsedApplicationKey = applicationKey;

		services.AddService<IGenderService, GenderService>(url, applicationKey);
		services.AddService<ISessionService, SessionService>(url, applicationKey);
		services.AddService<ILoginUserService, LoginUserService>(url, applicationKey);
		services.AddService<IRegisteredUserService, RegisteredUserService>(url, applicationKey);
		services.AddService<IAddressService, AddressService>(url, applicationKey);
		services.AddService<ICountryService, CountryService>(url, applicationKey);
		services.AddService<IStateService, StateService>(url, applicationKey);
		services.AddService<IApplicationService, ApplicationService>(url, applicationKey);
		services.AddService<ILoginAttemptService, LoginAttemptService>(url, applicationKey);
		services.AddService<IRoleService, RoleService>(url, applicationKey);
		services.AddService<IUserService, UserService>(url, applicationKey);

		//m:n
		services.AddService<IApplicationRoleService, ApplicationRoleService>(url, applicationKey);
		services.AddService<IRegisteredUserRoleService, RegisteredUserRoleService>(url, applicationKey);
		return services;
	}

	/// <summary>
	/// Implements the authorization for the UserServiceAPI.
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddUserServiceAuthorization(this IServiceCollection services) {
		services.AddAuthenticationCore();

		services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

		services.AddScoped<IdentityProvider>();

		return services;
	}

	/// <summary>
	/// Adds a scoped service of type <typeparamref name="T1"/> with an implementation type of <typeparamref name="T2"/> to the
	/// specified <see cref="IServiceCollection"/> with a base address of <paramref name="url"/> and an authorization header of
	/// <paramref name="applicationKey"/>.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="url"></param>
	/// <param name="applicationKey"></param>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <returns><see cref="IServiceCollection"/></returns>
	private static IServiceCollection AddService<T1, T2>(this IServiceCollection services, string url, string applicationKey) where T1 : class where T2 : class, T1 {
		services.AddHttpClient<T1, T2>(client => {
			client.BaseAddress = new Uri(url);
			client.DefaultRequestHeaders.Add("ApplicationKey", applicationKey);
		});

		return services;
	}
}