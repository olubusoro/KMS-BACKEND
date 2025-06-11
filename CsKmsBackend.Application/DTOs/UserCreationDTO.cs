using CsKmsBackend.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record UserCreationDTO(
		int Id,
		[Required] string Name,
		[Required, EmailAddress] string Email,
		[Required, MinLength(8, ErrorMessage = "Password must be greather than or equal to 8 characters")] string Password,
		[Required] UserRole Role,
		[Required, MinLength(1, ErrorMessage = "Please pick one department")] List<int> DepartmentIds
		);
}
