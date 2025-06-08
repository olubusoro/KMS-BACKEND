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

builder.Services.AddInfrastructureService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

if (args.Contains("--seed"))
{
	Console.WriteLine("?? Running seed process...");
	await SeedUsers.SeedAsync(app.Services);
	Console.WriteLine("? Seeding complete.");
	return; // exit after seeding
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseInfrastructurePolicy();

app.MapControllers();

app.Run();
