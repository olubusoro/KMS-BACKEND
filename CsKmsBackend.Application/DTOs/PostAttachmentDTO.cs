namespace CsKmsBackend.Application.DTOs
{
	public record PostAttachmentDTO(
		int Id,
		string OriginalFileName,
		string ContentType
		);
}
