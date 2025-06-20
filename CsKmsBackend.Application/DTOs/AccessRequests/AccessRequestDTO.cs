using CsKmsBackend.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.AccessRequests
{
	public record AccessRequestDTO(
		int Id,
		string PostTitle,
		string RequesterName,
		string Status,
		string Reason
		);
}
