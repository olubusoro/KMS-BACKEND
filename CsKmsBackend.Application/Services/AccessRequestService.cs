using CsKmsBackend.Application.DTOs.AccessRequests;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CsKmsBackend.Application.Services
{
	public class AccessRequestService(IAccessRequestRepository accessRequestRepo, IAuditLoggerService logger) : IAccessRequestService
	{
		public async Task<ResponseKms> ApproveRequestAsync(int userId, int id)
		{
			var result = await accessRequestRepo.ApproveAsync(id);
			if (result.Flag)
			{
				await logger.LogAsync(Domain.Models.Enums.ActionType.ApproveAccess, userId, Domain.Models.Enums.EntityType.AccessRequest);
			}
			return result;
		}

		public async Task<ResponseKms> CreateRequestAsync(int userId,AccessRequestCreationDTO accessRequestDTO)
		{
			var request = accessRequestDTO.ToEntity();
			request.UserId = userId;
			var result = await accessRequestRepo.CreateAsync(request);
			if (result.Flag)
			{
				await logger.LogAsync(Domain.Models.Enums.ActionType.RequestAccess, userId, Domain.Models.Enums.EntityType.AccessRequest);
			}
			return result;
		}

		public async Task<ResponseKms> DeleteRequestAsync(int userId, int id)
		{
			var result = await accessRequestRepo.DeleteAsync(id);
			if (result.Flag)
			{
				await logger.LogAsync(Domain.Models.Enums.ActionType.Delete, userId, Domain.Models.Enums.EntityType.AccessRequest);
			}
			return result;
		}

		public async Task<ResponseKms> DenyRequestAsync(int userId, int id)
		{
			var result = await accessRequestRepo.DenyAsync(id);
			if (result.Flag)
			{
				await logger.LogAsync(Domain.Models.Enums.ActionType.DenyAccess, userId, Domain.Models.Enums.EntityType.AccessRequest);
			}
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
