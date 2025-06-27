using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
	public interface IFeedbackRepository
	{
		Task<ResponseKms> AddAsync(Feedback feedback);
		Task<IEnumerable<Feedback>> GetAllAsync();
		Task<Feedback?> GetByIdAsync(int id);
	}
}
