using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Services
{
    public class UserService(IUserRepository userRepo) : IUserService
	{
		public async Task<ResponseKms> ChangeUserPasswordAsync(int id, ChangePasswordDTO changePasswordDTO)
		{
			var user = await userRepo.FindByIdAsync(id);
			if(!BCrypt.Net.BCrypt.Verify(changePasswordDTO.OldPassword, user.Password))
				return new ResponseKms(false, "old password does not match!");
			if(changePasswordDTO.NewPassword != changePasswordDTO.ConfirmNewPassword)
				return new ResponseKms(false, "confirm new password again!");
			var result = await userRepo.ChangePasswordAsync(user!, changePasswordDTO.NewPassword);
			return result;
		}

		public async Task<ResponseKms> CreateUserAsync(UserCreationDTO userCreationDTO)
		{
			var user = userCreationDTO.ToEntity();
			var selectedDepartments = await userRepo.GetDepartmentsByIdsAsync(userCreationDTO.DepartmentIds);
			user.Departments = selectedDepartments;
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
			var user = await userRepo.GetByAsync(u=>u.Id == id);
			return user is not null ? user.ToDTO() : null;
		}

		public async Task<ResponseKms> GetUserRoleByIdAsync(int id)
		{
			var user = await userRepo.GetByAsync(u => u.Id == id);
			var userRole = user?.Role.ToString();
			return userRole is not null 
				? new ResponseKms(true, userRole) 
				: new ResponseKms(false, "error occured");

		}

		public async Task<ResponseKms> ResetUserPasswordAsync(int id)
		{
			var user = await userRepo.FindByIdAsync(id);
			var result = await userRepo.ResetPasswordAsync(user!);
			return result;
		}

		public async Task<ResponseKms> UpdateUserAsync(UserUpdateDTO userDTO)
		{
			var user = userDTO.ToEntity();
			var selectedDepartments = await userRepo.GetDepartmentsByIdsAsync(userDTO.DepartmentIds);
			if(selectedDepartments.Count > 0) { 
				user.Departments.Clear();
				user.Departments = selectedDepartments;
			}
			return await userRepo.UpdateAsync(user);
		}
	}
}
