using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.DepartmentDTOs
{
	public record DepartmentUpdateDTO(
		[Required] int Id,
		[Required] string Name,
		string Description
		);
}
