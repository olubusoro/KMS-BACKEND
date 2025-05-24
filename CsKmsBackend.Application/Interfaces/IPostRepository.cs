using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
	public interface IPostRepository : IGenericInterface<Post>{
		Task<IEnumerable<Post>> SearchAsync(string search);
	}
}
