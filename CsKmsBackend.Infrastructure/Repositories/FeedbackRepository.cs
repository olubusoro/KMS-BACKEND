using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CsKmsBackend.Infrastructure.Repositories
{
	public class FeedbackRepository(KmsDbContext context) : IFeedbackRepository
	{
		public async Task<ResponseKms> AddAsync(Feedback feedback)
		{
			try
			{
				context.Feedbacks.Add(feedback);
				await context.SaveChangesAsync();
				return new(true, "Thank you for your feedback!");
			}
			catch
			{
				return new(false, "error occured while sending feedback");
			}
		}

		public async Task<IEnumerable<Feedback>> GetAllAsync()
		{
			try
			{
				var feedbacks = await context.Feedbacks
					 .Include(f => f.User)
					.AsNoTracking().OrderByDescending(f => f.CreatedAt)
					.ToListAsync();
				return feedbacks.Count > 0 ? feedbacks : [];
			}
			catch
			{
				return [];
			}
		}

		public async Task<Feedback?> GetByIdAsync(int id)
		{
			try
			{
				var feedback = await context.Feedbacks
					.Include(f=>f.User).FirstOrDefaultAsync(f => f.Id == id);

				return feedback.Id > 0 ? feedback : null;
			}
			catch
			{
				return null;
			}
		}
	}
}
