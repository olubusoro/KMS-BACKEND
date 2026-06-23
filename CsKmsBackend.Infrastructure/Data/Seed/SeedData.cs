using CsKmsBackend.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System;
using CsKmsBackend.Application.DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;

namespace CsKmsBackend.Infrastructure.Data.Seed
{
    public static class SeedData
	{
		public static async Task SeedAsync(IServiceProvider services)
		{
			using var scope = services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<KmsDbContext>();
			await db.Database.MigrateAsync();

			// Users
			if (!db.Users.Any())
			{
				var seedFile = Path.Combine(AppContext.BaseDirectory, "Data", "Seed", "users.json");
				var json = await File.ReadAllTextAsync(seedFile);
				var users = JsonSerializer.Deserialize<List<UserCreationDTO>>(json);

				foreach (var u in users)
				{
					db.Users.Add(new User
					{
						Name = u.Name,
						Email = u.Email,
						Password = BCrypt.Net.BCrypt.HashPassword(u.Password),
						Departments = db.Departments.Where(d=>u.DepartmentIds.Contains(d.Id)).ToList(),//(IList<Department>)u.DepartmentIds,
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
