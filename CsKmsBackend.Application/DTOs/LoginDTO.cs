using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record LoginDTO(
		[Required, EmailAddress] string Email,
		[Required] string Password);
}
