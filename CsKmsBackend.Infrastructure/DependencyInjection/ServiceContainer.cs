using CsKmsBackend.Application.DependencyInjection;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Infrastructure.Data;
using CsKmsBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CsKmsBackend.Infrastructure.DependencyInjection
{
	public static class ServiceContainer
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
		{
			// DbContext Connection
			services.AddDbContext<KmsDbContext>(options => options.UseSqlServer(
				config.GetConnectionString("ExpressConnection")
				)); //, sqlserverOption => sqlserverOption.EnableRetryOnFailure()));

			// Dependency Injection
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddApplicationService();

			// JWT service
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer("Bearer", options =>
				{
					var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
					string issuer = config.GetSection("Authentication:Issuer").Value!;
					string audience = config.GetSection("Authentication:Audience").Value!;

					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = issuer,
						ValidAudience = audience,
						IssuerSigningKey = new SymmetricSecurityKey(key)
					};
				});

			services.AddCors(options =>
			{
				options.AddPolicy("AllowReactApp",
					policy => policy
						.WithOrigins("http://localhost:5173") // React dev server
						.AllowAnyHeader()
						.AllowAnyMethod());
			});

			return services;
		}
	}
}
