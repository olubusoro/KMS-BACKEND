using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
	public interface IUserService
	{
		Task<ResponseKms> CreateUserAsync(UserCreationDTO userCreationDTO);
		Task<ResponseKms> UpdateUserAsync(UserUpdateDTO userDTO);
		Task<ResponseKms> DeleteUserAsync(int id);
		Task<IEnumerable<UserDTO>> GetAllUsersAsync();
		Task<UserDTO?> GetUserByIdAsync(int id);
		Task<UserDTO?> GetUserByEmailAsync(string email);
		Task<ResponseKms> ChangeUserPasswordAsync(int id,  ChangePasswordDTO changePasswordDTO);
		Task<ResponseKms> ResetUserPasswordAsync(int id);
	}
}
