using CsKmsBackend.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record UserDTO(
		int Id,
		[Required] string Name,
		[Required] string Email,
		[Required] UserRole Role,
		[Required, MinLength(1, ErrorMessage = "Please pick one department")] List<int> Departments,
		DateTime CreatedAt
		);
}
