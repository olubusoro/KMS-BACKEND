using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.DTOs.FeedbackDTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Services
{
	public class FeedbackService(IFeedbackRepository feedbackRepo) : IFeedbackService
	{
		public async Task<ResponseKms> AddFeedbackAsync( int userId,FeedbackCreateDTO feedbackDTO)
		{
			var feedback = feedbackDTO.ToEntity();
			feedback.UserId = userId;
			var result = await feedbackRepo.AddAsync(feedback);
			return result;
		}

		public async Task<IEnumerable<FeedbackDTO>> GetAllFeedbackAsync()
		{
			var feedbacks = await feedbackRepo.GetAllAsync();
			return feedbacks.ToDTO();
		}

		public async Task<FeedbackDTO?> GetFeedbackByIdAsync(int id)
		{
			var feedback = await feedbackRepo.GetByIdAsync(id);
			return feedback?.ToDTO();
		}
	}
}
