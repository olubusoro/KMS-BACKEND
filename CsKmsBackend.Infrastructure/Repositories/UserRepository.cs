using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CsKmsBackend.Infrastructure.Repositories
{
	public class UserRepository(KmsDbContext context) : IUserRepository
	{
		public async Task<ResponseKms> ChangePasswordAsync(User user, string password)
		{
			try { 
				user.Password = BCrypt.Net.BCrypt.HashPassword(password);
				context.Users.Update(user);
				await context.SaveChangesAsync();
				return new ResponseKms(true, "Password Changed successfully");
			}
			catch {
				return new ResponseKms(false, "Error occurred while trying to change password");
			}
		}

		public async Task<ResponseKms> CreateAsync(User entity)
		{
			try
			{
				var getUser = await GetByAsync(u=> u.Email ==  entity.Email);
				if (getUser is not null)
					return new ResponseKms(false, "User aleady exists");
				entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
				var user = context.Users.Add(entity).Entity;

				await context.SaveChangesAsync();
				return user is not null && user.Id > 0 ? new ResponseKms(true, "user created successfully") 
					: new ResponseKms(false, "Error occured while creating user");
			}
			catch(Exception ex) {
				return new ResponseKms(false, ex.Message);
			}
		}

		public async Task<ResponseKms> DeleteAsync(int id)
		{
			try
			{
				var user = await FindByIdAsync(id);
				if(user is null)
					return new ResponseKms(false, $"user with id {id} not found");
				context.Users.Remove(user);
				await context.SaveChangesAsync();
				return new ResponseKms(true, $"User with id {id} has been deleted successfully");
			}catch(Exception ex)
			{
				return new ResponseKms(false, ex.Message);
			}
		}

		public async Task<User?> FindByIdAsync(int id)
		{
			try
			{
				var user = await context.Users.FindAsync(id);
				if(user is null)
					return null;
				return user;
			}catch
			{
				return null;
			}
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			try
			{
				var users = await context.Users.AsNoTracking().ToListAsync();
				return users.Any() ? users : Enumerable.Empty<User>();
			}
			catch
			{
				return Enumerable.Empty<User>();
			}
		}

		public async Task<User?> GetByAsync(Expression<Func<User, bool>> predicate)
		{
			try
			{
				var user = await context.Users.Where(predicate).FirstOrDefaultAsync();
				return user is not null && user.Id > 0 ? user : null;
			}
			catch
			{
				return null;
			}
		}

		public async Task<ResponseKms> ResetPasswordAsync(User user)
		{
			try
			{
				var password = "GenericPassword123";
				user.Password = BCrypt.Net.BCrypt.HashPassword(password);
				context.Users.Update(user);
				await context.SaveChangesAsync();
				return new ResponseKms(true, $"Password has been reset successfully");
			}
			catch
			{
				return new ResponseKms(false, "Error occurred while trying to reset password");
			}
		}

		//private static string RandomPasswordGenerator(int length=10, int minNumbers = 2, int maxNumbers = 3) 
		//{
		//	if (length < minNumbers)
		//		throw new ArgumentException("Length must be greater than or equal to the minimum number of digits.");

		//	if (minNumbers > maxNumbers)
		//		throw new ArgumentException("Minimum number of digits cannot be greater than the maximum.");

		//	if (maxNumbers > length - 2)
		//		throw new ArgumentException("Maximum number of digits cannot be greater than the length.");

		//	Random random = new Random();
		//	const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		//	const string numbers = "0123456789";

		//	int countOfNumbers = random.Next(minNumbers, maxNumbers + 1);
		//	int numberOfChars = length - countOfNumbers;

		//	StringBuilder result = new StringBuilder(length);

		//	// Add random letters
		//	for (int i = 0; i < numberOfChars; i++)
		//	{
		//		result.Append(chars[random.Next(chars.Length)]);
		//	}

		//	// Add random numbers at random positions
		//	for (int i = 0; i < countOfNumbers; i++)
		//	{
		//		int insertPosition = random.Next(result.Length + 1);
		//		result.Insert(insertPosition, numbers[random.Next(numbers.Length)]);
		//	}

		//	return result.ToString();
		//}

		public async Task<ResponseKms> UpdateAsync(User entity)
		{
			try
			{
				var user = await FindByIdAsync(entity.Id);
				if (user is null)
					return new ResponseKms(false, "user not found");
				entity.CreatedAt = user.CreatedAt;
				entity.Password = user.Password;
				context.Entry(user).State = EntityState.Detached;
				context.Users.Update(entity);
				await context.SaveChangesAsync();
				return new ResponseKms(true, "user successfully updated");
			}
			catch
			{
				return new ResponseKms(false, "Error ocurred while trying to update user");
			}
		}
	}
}
