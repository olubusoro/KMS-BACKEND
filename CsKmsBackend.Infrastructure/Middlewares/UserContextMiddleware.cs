using CsKmsBackend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CsKmsBackend.Infrastructure.Middlewares
{
	public class UserContextMiddleware(RequestDelegate next)
	{
		public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService)
		{
			if (context.User.Identity?.IsAuthenticated == true)
			{
				var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)
								?? context.User.FindFirst("sub");

				if (userIdClaim != null)
				{
					currentUserService.UserId = int.Parse(userIdClaim.Value);
				}
			}

			await next(context);
		}
	}
}
