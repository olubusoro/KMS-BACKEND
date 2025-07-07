namespace CsKmsBackend.Application.Interfaces
{
	public interface IEmailService
	{
		Task SendWelcomeEmailAsync(string toEmail, string username, string tempPassword);
		Task SendPasswordResetEmailAsync(string toEmail, string username, string newPassword);
	}
}
