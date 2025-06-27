using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.PostDTOs
{
	public record PostListDTO(
		int Id,
		string Title,
		string Description,
		string CategoryName,
		string UserName,
		DateTime CreatedAt
		);
}
