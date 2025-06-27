using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Domain.Models.Enums;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CsKmsBackend.Infrastructure.Repositories
{
	public class LogRepository(KmsDbContext context) : ILogRepository
	{
		public async Task<Log?> FindByIdAsync(int id)
		{
			var log = await context.Logs.FindAsync(id);
			return log;
		}

		public async Task<IEnumerable<Log>> GetAllAsync()
		{
			var logs = await context.Logs.AsNoTracking().ToListAsync();
			return logs;
		}

		public async Task LogCreateAsync(ActionType actionType, int performedByUserId, EntityType entityType)
		{
			try
			{
				var userName = context.Users.AsNoTracking().FirstOrDefault(u => u.Id == performedByUserId).Name;
				if (entityType.Equals(EntityType.Post)) {
					var post = await context.Posts.Where(p => p.UserId == performedByUserId).OrderByDescending(p => p.CreatedAt).FirstOrDefaultAsync();
					var log = new Log
					{
						UserName = userName,
						Action = actionType.ToString(),
						PerformedBy = performedByUserId,
						EntityType = entityType.ToString(),
						EntityId = post.Id, //entityId
						PostTitle = post.Title
					};
					context.Logs.Add(log);
				}
				else if (entityType.Equals(EntityType.AccessRequest))
				{
					var ar = await context.AccessRequests.Where(r => r.UserId == performedByUserId).Include(r=>r.RequestedPost).OrderByDescending(p => p.Id).FirstOrDefaultAsync();
					var log = new Log
					{
						UserName= userName,
						Action = actionType.ToString(),
						PerformedBy = performedByUserId,
						EntityType = entityType.ToString(),
						EntityId = ar.Id, //entityId
						PostTitle = ar.RequestedPost.Title
					};
					context.Logs.Add(log);
				}

				await context.SaveChangesAsync();
			}
			catch
			{
				Console.WriteLine("Failed to Log");
			}
		}
		public async Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType, int EntityId)
		{
			try
			{
				var userName = context.Users.AsNoTracking().FirstOrDefault(u => u.Id == performedByUserId).Name;
				string postTitle;
				if (entityType.Equals(EntityType.Post))
					postTitle = context.Posts.FirstOrDefault(p => p.Id == EntityId).Title;
				else
					postTitle = context.AccessRequests.Include(r => r.RequestedPost).FirstOrDefault(r => r.Id == EntityId).RequestedPost.Title;
				
				var log = new Log
				{
					UserName = userName,
					PostTitle = postTitle,
					Action = actionType.ToString(),
					PerformedBy = performedByUserId,
					EntityType = entityType.ToString(),
					EntityId = EntityId
				};
				context.Logs.Add(log);

				await context.SaveChangesAsync();
			}
			catch
			{
				Console.WriteLine("Failed to Log");
			}
		}
	}
}
