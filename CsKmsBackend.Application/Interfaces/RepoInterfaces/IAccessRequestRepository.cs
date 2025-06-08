using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface IAccessRequestRepository : IGenericInterface<AccessRequest>
    {
        Task<ResponseKms> ApproveAsync(int id);
        Task<ResponseKms> DenyAsync(int id);
    }
}
