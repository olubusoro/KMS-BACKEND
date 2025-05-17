using CsKmsBackend.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System;
using CsKmsBackend.Application.DTOs;

namespace CsKmsBackend.Infrastructure.Data.Seed
{
	public static class SeedUsers
	{
		public static async Task SeedAsync(IServiceProvider services)
		{
			using var scope = services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<KmsDbContext>();

			if (!db.Users.Any())
			{
				var json = await File.ReadAllTextAsync("../CsKmsBackend.Infrastructure/Data/Seed/users.json");
				var users = JsonSerializer.Deserialize<List<UserCreationDTO>>(json);

				foreach (var u in users)
				{
					db.Users.Add(new User
					{
						Name = u.Name,
						Email = u.Email,
						Password = BCrypt.Net.BCrypt.HashPassword(u.Password),
						Departments = u.Departments,
						Role = u.Role,
						CreatedAt = DateTime.UtcNow,
					});
				}

				await db.SaveChangesAsync();
			}
			else
			{
				Console.WriteLine("Users already exists");
			}
		}
	}
}
