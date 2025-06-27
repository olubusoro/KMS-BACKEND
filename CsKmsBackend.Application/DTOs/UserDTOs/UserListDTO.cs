using CsKmsBackend.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.UserDTOs
{
	public record UserListDTO(
		int Id,
		string Name,
		string Email,
		string Role,
		List<string> Departments,
		DateTime CreatedAt
		);
}
