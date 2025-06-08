using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces.RepoInterfaces
{
    public interface IUserRepository : IGenericInterface<User>
    {
        Task<ResponseKms> ChangePasswordAsync(User user, string password);
        Task<ResponseKms> ResetPasswordAsync(User user);
    }
}
