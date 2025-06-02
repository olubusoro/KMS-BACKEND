using CsKmsBackend.Domain.Models;
using System.Linq.Expressions;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface IGenericInterface<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindByIdAsync(int id);
        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<ResponseKms> CreateAsync(T entity);
        Task<ResponseKms> UpdateAsync(T entity);
        Task<ResponseKms> DeleteAsync(int id);
    }
}
