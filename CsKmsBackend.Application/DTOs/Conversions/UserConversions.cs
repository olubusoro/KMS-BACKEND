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
			Role = userCreationDTO.Role,
			Departments = userCreationDTO.Departments
		};

		public static User ToEntity(this UserDTO userDTO) => new User {
			Id = userDTO.Id,
			Name = userDTO.Name,
			Email = userDTO.Email,
			Role = userDTO.Role,
			Departments = userDTO.Departments
		};

		public static UserDTO ToDTO(this User user) => new UserDTO(user.Id,
								user.Name,
								user.Email,
								user.Role,
								user.Departments,
								user.CreatedAt);

		public static IEnumerable<UserDTO> ToDTO(this IEnumerable<User> users)
		{
			var userDTOs = new List<UserDTO>();
			foreach (var user in users)
			{
				userDTOs.Add(new UserDTO(user.Id,
								user.Name,
								user.Email,
								user.Role,
								user.Departments,
								user.CreatedAt));
			}
			return userDTOs;
		}
	}
}
