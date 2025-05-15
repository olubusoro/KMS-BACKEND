using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Services
{
	public class UserService(IUserRepository userRepo) : IUserService
	{
		public async Task<ResponseKms> CreateUserAsync(UserCreationDTO userCreationDTO)
		{
			var user = userCreationDTO.ToEntity();
			var response = await userRepo.CreateAsync(user);
			return response;
		}
		
		public async Task<ResponseKms> DeleteUserAsync(int id)
		{
			return await userRepo.DeleteAsync(id);;
		}

		public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
		{
			var users = await userRepo.GetAllAsync();
			return users.ToDTO();
		}

		public async Task<UserDTO?> GetUserByEmailAsync(string email)
		{
			var user = await userRepo.GetByAsync(u => u.Email == email);
			return user is not null ? user.ToDTO() : null;
		}

		public async Task<UserDTO?> GetUserByIdAsync(int id)
		{
			var user = await userRepo.FindByIdAsync(id);
			return user is not null ? user.ToDTO() : null;
		}

		public async Task<ResponseKms> UpdateUserAsync(UserDTO userDTO)
		{
			var user = userDTO.ToEntity();
			return await userRepo.UpdateAsync(user);
		}
	}
}
