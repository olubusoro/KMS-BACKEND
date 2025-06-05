using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Application.Interfaces
{
	public interface IAuditLoggerService
	{
		Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType);
		Task<IEnumerable<LogDTO>> GetAllLogsAsync();
		Task<LogDTO?> FindLogByIdAsync(int id);
	}
}
