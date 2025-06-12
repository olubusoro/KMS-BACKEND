using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record CreateDepartmentDTO(
		[Required] string Name,
		string Description
	);
}
