using CsKmsBackend.Application.DTOs.FeedbackDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs.Conversions
{
    public static class FeedbackConversions
	{
		public static Feedback ToEntity(this FeedbackCreateDTO feedbackCreateDTO) => new()
		{
			Subject = feedbackCreateDTO.Subject,
			Message = feedbackCreateDTO.Message,
			Type = feedbackCreateDTO.Type
		};

		public static FeedbackDTO ToDTO(this Feedback feedback) => new(
			feedback.Id,
			feedback.Subject,
			feedback.Message,
			feedback.Type.ToString(),
			feedback.CreatedAt,
			feedback.User.Name
			);

		public static IEnumerable<FeedbackDTO> ToDTO(this IEnumerable<Feedback> feedbacks)
		{
			var feedbackDtos = new List<FeedbackDTO>();
			foreach ( var feedback in feedbacks )
				feedbackDtos.Add( ToDTO( feedback ) );
			return feedbackDtos;
		}
	}
}
