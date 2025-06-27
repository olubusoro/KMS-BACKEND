namespace CsKmsBackend.Application.DTOs.FeedbackDTOs
{
    public record FeedbackDTO(
        int Id,
        string Subject,
        string Message,
        string Type,
        DateTime CreatedAt,
        string Username
        );
}
