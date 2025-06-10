using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record CreateDepartmentDTO(
		int Id,
		[Required] string Name,
		string Description
	);
}
