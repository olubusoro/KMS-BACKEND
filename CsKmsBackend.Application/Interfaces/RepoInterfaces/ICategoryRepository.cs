using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface ICategoryRepository : IGenericInterface<Category>
    {
        Task<IEnumerable<Category>> GetAllByUserIdAsync(int  userId);
    }
}
