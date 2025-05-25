using CsKmsBackend.Application.DTOs.AccessRequests;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Services
{
	public class AccessRequestService(IAccessRequestRepository accessRequestRepo) : IAccessRequestService
	{
		public async Task<ResponseKms> ApproveRequestAsync(int id)
		{
			var result = await accessRequestRepo.ApproveAsync(id);
			return result;
		}

		public async Task<ResponseKms> CreateRequestAsync(AccessRequestCreationDTO accessRequestDTO)
		{
			var request = accessRequestDTO.ToEntity();
			var result = await accessRequestRepo.CreateAsync(request);
			return result;
		}

		public async Task<ResponseKms> DeleteRequestAsync(int id)
		{
			var result = await accessRequestRepo.DeleteAsync(id);
			return result;
		}

		public async Task<ResponseKms> DenyRequestAsync(int id)
		{
			var result = await accessRequestRepo.DenyAsync(id);
			return result;
		}

		public async Task<IEnumerable<AccessRequestDTO>> GetAllRequestAsync()
		{
			var requests = await accessRequestRepo.GetAllAsync();
			return requests.Any() ? requests.ToDTO() : [];
		}

		public async Task<AccessRequestDTO?> GetRequestAsync(int id)
		{
			var request = await accessRequestRepo.GetByAsync(r => r.Id == id);
			return request?.ToDTO();
		}

		public async Task<ResponseKms> UpdateRequestReasonAsync(AccessRequestDTO accessRequestDTO)
		{
			var request = accessRequestDTO.ToEntity();
			var result = await accessRequestRepo.UpdateAsync(request);
			return result;
		}
	}
}
