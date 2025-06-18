using CsKmsBackend.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.AccessRequests
{
	public record AccessRequestUpdateDTO(
		[Required] int Id,
		[Required] int PostId,
		[Required] int UserId,
		Status Status,
		string Reason
		);
}
