using CsKmsBackend.Application.DTOs.AccessRequests;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
	public interface IAccessRequestService
	{
		Task<ResponseKms> CreateRequestAsync(int userId, AccessRequestCreationDTO accessRequestDTO);
		Task<ResponseKms> UpdateRequestReasonAsync(AccessRequestUpdateDTO accessRequestDTO);
		Task<ResponseKms> ApproveRequestAsync(int userId, int id);
		Task<ResponseKms> DenyRequestAsync(int userId, int id);
		Task<ResponseKms> DeleteRequestAsync(int userId, int id);
		Task<IEnumerable<AccessRequestDTO>> GetAllRequestAsync();
		Task<AccessRequestDTO?> GetRequestAsync(int id);
		Task<IEnumerable<AccessRequestDTO>> GetRequestsForPrivatePostsAsync(int creatorUserId);
		Task<IEnumerable<AccessRequestDTO>> GetRequestsForDepartmentAdminsAsync(int deptAdminId);
	}
}
