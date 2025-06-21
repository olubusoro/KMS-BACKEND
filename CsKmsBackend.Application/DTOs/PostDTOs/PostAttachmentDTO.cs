namespace CsKmsBackend.Application.DTOs.PostDTOs
{
    public record PostAttachmentDTO(
        int Id,
        string OriginalFileName,
        string ContentType
        );
}
