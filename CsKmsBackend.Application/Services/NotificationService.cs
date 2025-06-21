using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.DTOs.NotificationDTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Services
{
	public class NotificationService(
		INotificationRepository notificationRepo) : INotificationService
	{
		public async Task<ResponseKms> CreateNotificationAsync(Notification notification)
		{
			return await notificationRepo.CreateNotificationAsync(notification);
		}

		public async Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync(int userId)
		{
			var notifications = await notificationRepo.GetUserNotificationsAsync(userId);
			return notifications.ToDTO();
		}

		public async Task<ResponseKms> MarkAsReadAsync(int notificationId, int userId)
		{
			return await notificationRepo.MarkAsReadAsync(notificationId, userId);
		}
	}
}
