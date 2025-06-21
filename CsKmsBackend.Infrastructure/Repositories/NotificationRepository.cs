using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CsKmsBackend.Infrastructure.Repositories
{
	public class NotificationRepository(KmsDbContext context) : INotificationRepository
	{
		public async Task<ResponseKms> CreateNotificationAsync(Notification notification)
		{
			try
			{
				context.Notifications.Add(notification);
				await context.SaveChangesAsync();
				return new ResponseKms(true, "Notification created successfully");
			}
			catch {
				return new ResponseKms(false, "Error occurred while creating Notification");
			}
		}

		public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
		{
			try
			{
				var notifications = await context.Notifications
					.Where(n => n.UserId == userId && n.IsRead == false)
					.OrderByDescending(n => n.CreatedAt).ToListAsync();
				return notifications.Count != 0 ? notifications : [];
			}
			catch
			{
				return [];
			}
		}

		public async Task<ResponseKms> MarkAsReadAsync(int notificationId, int userId)
		{
			try
			{
				var notification = await context.Notifications
					.FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
				if (notification is null)
					return new ResponseKms(false, "Notification not found");
				if ( !notification.IsRead)
				{
					notification.IsRead = true;
					await context.SaveChangesAsync();
					return new ResponseKms(true, "Notication read successfully");
				}
				return new ResponseKms(false, "Notifaction already marked as read");
			}
			catch
			{
				return new(false, "Error occurred while marking notification as read");
			}
		}
	}
}
