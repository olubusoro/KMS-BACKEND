using CsKmsBackend.Domain.Models;
using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface ILogRepository
    {
        Task LogAsync(ActionType actionType, int performedByUserId, EntityType entityType);
        Task<IEnumerable<Log>> GetAllAsync();
        Task<Log?> FindByIdAsync(int id);
    }

}
