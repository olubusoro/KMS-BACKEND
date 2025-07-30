using AspNetCoreRateLimit;
using CsKmsBackend.Infrastructure.Data.Seed;
using CsKmsBackend.Infrastructure.DependencyInjection;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var jwtSecurityScheme = new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = JwtBearerDefaults.AuthenticationScheme,
		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};

	options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{jwtSecurityScheme, new string[]{} }
		});
});
builder.Services.AddOptions();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInfrastructureService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (args.Contains("--seed"))
{
	Console.WriteLine("?? Running seed process...");
	await SeedData.SeedAsync(app.Services);
	Console.WriteLine("? Seeding complete.");
	return; // exit after seeding
}

var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
	app.Urls.Add($"http://*:{port}");
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseAuthentication();

app.UseAuthorization();

app.UseInfrastructurePolicy();

app.MapControllers();

app.Run();
