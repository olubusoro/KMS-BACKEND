using CsKmsBackend.Application.DTOs.UserDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs.Conversions
{
    public static class UserConversions
	{
		public static User ToEntity(this UserCreationDTO userCreationDTO) => new User
		{
			Id = userCreationDTO.Id,
			Name = userCreationDTO.Name,
			Password = userCreationDTO.Password,
			Email = userCreationDTO.Email,
			Role = userCreationDTO.Role
		};

		public static User ToEntity(this UserUpdateDTO userDTO) => new User {
			Id = userDTO.Id,
			Name = userDTO.Name,
			Email = userDTO.Email,
			Role = userDTO.Role
		};

		public static UserDTO ToDTO(this User user) => new UserDTO(user.Id,
								user.Name,
								user.Email,
								user.Role,
								user.Departments.Select(d=>d.Id).ToList(),
								user.Departments.Select(d=>d.Name).ToList(),
								user.CreatedAt);

		public static UserListDTO ToListDTO(this User user) => new(
			user.Id,
			user.Name,
			user.Email,
			user.Role.ToString(),
			user.Departments.Select(d=>d.Name).ToList(),
			user.CreatedAt
			);

		public static IEnumerable<UserListDTO> ToListDTO(this  IEnumerable<User> users)
		{
			var userListDTOs = new List<UserListDTO>();
			foreach (var user in users)
				userListDTOs.Add(ToListDTO(user));
			return userListDTOs;
		}

		public static IEnumerable<UserDTO> ToDTO(this IEnumerable<User> users)
		{
			var userDTOs = new List<UserDTO>();
			foreach (var user in users)
			{
				userDTOs.Add(ToDTO(user));
			}
			return userDTOs;
		}
	}
}
