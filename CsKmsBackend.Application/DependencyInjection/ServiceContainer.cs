using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CsKmsBackend.Application.DependencyInjection
{
	public static class ServiceContainer
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			// Dependency Injection
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IPostService, PostService>();
			services.AddScoped<IAccessRequestService, AccessRequestService>();


			return services;
		}
	}
}
