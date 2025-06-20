using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record PostDTO(
		[Required] int Id,
		[Required] string Title,
		string? Description,
		[Required] string Content,
		[Required] string Visibility,
		int? CategoryId,
		[Required] string UserName,
		DateTime CreatedAt,
		List<PostAttachmentDTO>? Attachments);
}
