using CsKmsBackend.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.FeedbackDTOs
{
	public record FeedbackCreateDTO(
		[Required, MaxLength(100)] string Subject,
		[Required, MinLength(1),MaxLength(1500)] string Message,
		[Required] FeedbackType Type
		);
}
