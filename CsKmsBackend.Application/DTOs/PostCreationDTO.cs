using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record PostCreationDTO(
		[Required] string Title,
		string? Description,
		[Required] string Content,
		[Required] string Visibility,
		int? CategoryId,
		List<IFormFile>? Attachments);
}
