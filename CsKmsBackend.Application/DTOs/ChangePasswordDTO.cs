using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
	public record ChangePasswordDTO(
		[Required] string OldPassword,
		[Required, MinLength(8, ErrorMessage = "Password must be greather than or equal to 8 characters")] string NewPassword,
		[Required] string ConfirmNewPassword);
}
