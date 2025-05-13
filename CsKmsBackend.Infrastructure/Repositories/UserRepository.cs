using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CsKmsBackend.Infrastructure.Repositories
{
	public class UserRepository(KmsDbContext context) : IUserRepository
	{
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
					return new ResponseKms(false, $"user with {id} not found");
				context.Users.Remove(user);
				await context.SaveChangesAsync();
				return new ResponseKms(true, $"User with {id} has been deleted successfully");
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

		public async Task<ResponseKms> UpdateAsync(User entity)
		{
			try
			{
				var user = await FindByIdAsync(entity.Id);
				if (user is null)
					return new ResponseKms(false, "user not found");
				entity.CreatedAt = user.CreatedAt;
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
