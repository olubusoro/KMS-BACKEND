using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models.Enums;
using CsKmsBackend.Application.DTOs.Conversions;

namespace CsKmsBackend.Application.Services
{
    public class AuditLoggerService(ILogRepository logRepo) : IAuditLoggerService
	{
		public async Task<LogDTO?> FindLogByIdAsync(int id)
		{
			var log = await logRepo.FindByIdAsync(id);
			return log?.ToDTO();
		}

		public async Task<IEnumerable<LogDTO>> GetAllLogsAsync()
		{
			var logs = await logRepo.GetAllAsync();
			return logs.Any() ? logs.ToDTO() : [];
		}

		public async Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType)
		{
			await logRepo.LogAsync(actionType, performedByUserId, entityType);
		}
	}
}
