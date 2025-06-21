using CsKmsBackend.Application.DTOs.NotificationDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs.Conversions
{
	public static class NotificationConversions
	{
		public static NotificationDTO ToDTO(this Notification notification) => new(
			notification.Id,
			notification.UserId,
			notification.Title,
			notification.Message,
			notification.IsRead,
			notification.CreatedAt
			);

		public static IEnumerable<NotificationDTO> ToDTO(this IEnumerable<Notification> notifications)
		{
			var notificationDTOs = new List<NotificationDTO>();
			foreach ( var notification in notifications)
				notificationDTOs.Add(ToDTO(notification));
			return notificationDTOs;
		}
	}
}
