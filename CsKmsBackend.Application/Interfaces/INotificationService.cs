using CsKmsBackend.Application.DTOs.NotificationDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
	public interface INotificationService
	{
		Task<ResponseKms> CreateNotificationAsync(Notification notification);
		Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync(int userId);
		Task<ResponseKms> MarkAsReadAsync(int notificationId, int userId);
		
	}
}
