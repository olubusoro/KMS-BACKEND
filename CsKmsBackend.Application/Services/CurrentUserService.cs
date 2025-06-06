using CsKmsBackend.Application.Interfaces;

namespace CsKmsBackend.Application.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		public int UserId { get; set; }
	}
}
