using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface IPostRepository : IGenericInterface<Post>
    {
        Task<IEnumerable<Post>> SearchAsync(string search);
		Task<Post?> GetByIdWithDetailsAsync(int postId);
		Task<AccessRequest?> GetAccessRequestAsync(int postId, int userId);
		Task<User?> GetUserWithDepartmentsAsync(int userId);
	}
}
