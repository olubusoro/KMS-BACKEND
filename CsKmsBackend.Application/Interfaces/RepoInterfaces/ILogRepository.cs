using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface ILogRepository
    {
        Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType);
    }

}
