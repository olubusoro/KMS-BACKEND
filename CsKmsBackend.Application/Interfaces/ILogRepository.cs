using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Application.Interfaces
{
	public interface ILogRepository
	{
		Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType);
	}

}
