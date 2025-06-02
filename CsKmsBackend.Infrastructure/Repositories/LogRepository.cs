using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Domain.Models.Enums;
using CsKmsBackend.Infrastructure.Data;

namespace CsKmsBackend.Infrastructure.Repositories
{
    public class LogRepository(KmsDbContext context) : ILogRepository
	{
		public async Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType)
		{
			try
			{
				if (entityType.Equals(EntityType.Post)) { 
					var log = new Log
					{
						Action = actionType.ToString(),
						PerformedBy = performedByUserId,
						EntityType = entityType.ToString(),
						EntityId = context.Posts.Where(p => p.UserId == performedByUserId).OrderByDescending(p => p.CreatedAt).FirstOrDefault().Id //entityId
					};
					context.Logs.Add(log);
				}
				else if (entityType.Equals(EntityType.AccessRequest))
				{
					var log = new Log
					{
						Action = actionType.ToString(),
						PerformedBy = performedByUserId,
						EntityType = entityType.ToString(),
						EntityId = context.AccessRequests.Where(r => r.UserId == performedByUserId).OrderByDescending(p => p.Id).FirstOrDefault().Id //entityId
					};
					context.Logs.Add(log);
				}
				
				await context.SaveChangesAsync();
			}
			catch{
				Console.WriteLine("Failed to Log");
			}
		}
	}
}
