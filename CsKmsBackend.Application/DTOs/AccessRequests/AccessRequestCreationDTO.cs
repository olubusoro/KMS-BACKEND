using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.AccessRequests
{
	public record AccessRequestCreationDTO(
		[Required] int PostId,
		//[Required] int UserId,
		string Reason
		);
}
