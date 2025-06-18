using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface IAccessRequestRepository : IGenericInterface<AccessRequest>
    {
		Task<IEnumerable<AccessRequest>> GetRequestsForPrivatePostsAsync(int creatorUserId);
		Task<IEnumerable<AccessRequest>> GetRequestsForDepartmentAdminsAsync(int deptAdminId);
		Task<ResponseKms> ApproveAsync(int id);
        Task<ResponseKms> DenyAsync(int id);
    }
}
