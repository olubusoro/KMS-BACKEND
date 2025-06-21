using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
	public interface INotificationRepository
	{
		Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);
		Task<ResponseKms> MarkAsReadAsync(int notificationId, int userId);
		Task<ResponseKms> CreateNotificationAsync(Notification notification);
	}
}
