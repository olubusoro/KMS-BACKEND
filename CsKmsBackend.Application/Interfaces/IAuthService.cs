using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
	public interface IAuthService
	{
		Task<ResponseKms> LoginAsync(LoginDTO request);
	}
}
