using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Application.Services
{
    public class AuditLoggerService(ILogRepository logRepo) : IAuditLoggerService
	{
		public async Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType)
		{
			await logRepo.LogAsync(actionType, performedByUserId, entityType);
		}
	}
}
