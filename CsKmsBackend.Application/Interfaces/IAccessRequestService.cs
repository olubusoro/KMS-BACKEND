using CsKmsBackend.Application.DTOs.AccessRequests;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
	public interface IAccessRequestService
	{
		Task<ResponseKms> CreateRequestAsync(AccessRequestCreationDTO accessRequestDTO);
		Task<ResponseKms> UpdateRequestReasonAsync(AccessRequestDTO accessRequestDTO);
		Task<ResponseKms> ApproveRequestAsync(int id);
		Task<ResponseKms> DenyRequestAsync(int id);
		Task<ResponseKms> DeleteRequestAsync(int id);
		Task<IEnumerable<AccessRequestDTO>> GetAllRequestAsync();
		Task<AccessRequestDTO?> GetRequestAsync(int id);
	}
}
