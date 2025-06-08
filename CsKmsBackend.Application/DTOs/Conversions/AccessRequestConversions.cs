using CsKmsBackend.Application.DTOs.AccessRequests;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs.Conversions
{
	public static class AccessRequestConversions
	{
		public static AccessRequest ToEntity(this AccessRequestCreationDTO accessRequestDTO) => new()
		{
			PostId = accessRequestDTO.PostId,
			Reason = accessRequestDTO.Reason,
		};

		public static AccessRequest ToEntity(this AccessRequestDTO accessRequestDTO) => new()
		{
			Id = accessRequestDTO.Id,
			PostId = accessRequestDTO.PostId,
			UserId = accessRequestDTO.UserId,
			Status = accessRequestDTO.Status,
			Reason = accessRequestDTO.Reason ?? ""
		};

		public static AccessRequestDTO ToDTO(this AccessRequest accessRequest) => new(
			accessRequest.Id,
			accessRequest.PostId,
			accessRequest.UserId,
			accessRequest.Status,
			accessRequest.Reason
			);
		public static IEnumerable<AccessRequestDTO> ToDTO(this IEnumerable<AccessRequest> accessRequests)
		{
			var accessRequestDTOs = new List<AccessRequestDTO>();

			foreach ( var accessRequest in accessRequests)
			{
				accessRequestDTOs.Add( ToDTO(accessRequest));
			}
			return accessRequestDTOs;
		}
	}
}
