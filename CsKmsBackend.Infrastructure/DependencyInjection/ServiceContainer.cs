using CsKmsBackend.Application.DependencyInjection;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Infrastructure.Data;
using CsKmsBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsKmsBackend.Infrastructure.DependencyInjection
{
	public static class ServiceContainer
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<KmsDbContext>(options => options.UseSqlServer(
				config.GetConnectionString("ExpressConnection")
				)); //, sqlserverOption => sqlserverOption.EnableRetryOnFailure()));

			// Dependency Injection
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddApplicationService();

			return services;
		}
	}
}
