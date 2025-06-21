using CsKmsBackend.Application.DTOs.NotificationDTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController(INotificationService notificationService,
		ICurrentUserService currentUser) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetNotifications()
		{
			var notificartions = await notificationService.GetUserNotificationsAsync(currentUser.UserId);
			return Ok(notificartions);
		}

		[HttpPatch("{id:int}/read")]
		public async Task<ActionResult<ResponseKms>> MarkAsRead(int id)
		{
			var response = await notificationService.MarkAsReadAsync(id,currentUser.UserId);
			return response.Flag ? Ok(response) : BadRequest(response);
		}
	}
}
