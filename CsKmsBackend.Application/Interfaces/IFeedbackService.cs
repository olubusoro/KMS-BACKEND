using CsKmsBackend.Application.DTOs.FeedbackDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
    public interface IFeedbackService
	{
		Task<ResponseKms> AddFeedbackAsync(int userId, FeedbackCreateDTO feedbackDTO);
		Task<IEnumerable<FeedbackDTO>> GetAllFeedbackAsync();
		Task<FeedbackDTO?> GetFeedbackByIdAsync(int id);
	}
}
