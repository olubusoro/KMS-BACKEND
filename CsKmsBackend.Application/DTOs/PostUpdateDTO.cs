using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record PostUpdateDTO(
		[Required] int Id,
		string? Title,
		string? Description,
		string? Content,
		string? Visibility
		);
}
