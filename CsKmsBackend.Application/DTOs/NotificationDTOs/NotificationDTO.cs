namespace CsKmsBackend.Application.DTOs.NotificationDTOs
{
	public record NotificationDTO(
		int Id,
		int UserId,
		string Title,
		string Message,
		bool IsRead,
		DateTime CreatedAt
		);
}
