using brevo_csharp.Api;
using brevo_csharp.Model;
using CsKmsBackend.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Task = System.Threading.Tasks.Task;

namespace CsKmsBackend.Application.Services
{
	public class EmailService(IConfiguration config) : IEmailService
	{
		public async Task SendWelcomeEmailAsync(string toEmail, string username, string tempPassword)
		{
			brevo_csharp.Client.Configuration.Default.AddApiKey("api-key", config["BREVO:APIKEY"]);
			var settings = config.GetSection("EmailSettings");
			var apiInstance = new TransactionalEmailsApi();
			var sender = new SendSmtpEmailSender(name: settings["SenderName"], email: settings["SenderEmail"]);
			var recipient = new SendSmtpEmailTo(toEmail, username);
			var to = new List<SendSmtpEmailTo>() { recipient};
			var htmlContent = WelcomeMessage(username, tempPassword);
			var sendSmtpEmail = new SendSmtpEmail(sender:sender,
				to:to,htmlContent:htmlContent,subject: "Welcome to CS-KMS!");

			try
			{
				// Send a transactional email
				CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
			}
			catch (Exception e)
			{
				Debug.Print("Exception when calling TransactionalEmailsApi.SendTransacEmailAsync " + e.Message);
			}
		}

		private string WelcomeMessage(string username, string tempPassword) => $@"
				  <div style='font-family:Segoe UI, Helvetica, Arial, sans-serif; max-width:600px; margin:auto; padding:20px; background-color:#f9f9f9; border:1px solid #ddd; border-radius:8px;'>
					<h2 style='color:#1F8A47;'>Welcome, {username}!</h2>
					<p style='font-size:16px; color:#333;'>Your account has been created by the Super Admin.</p>
    
					<p style='font-size:16px; color:#333;'>
					  <strong style='color:#333;'>Temporary Password:</strong>
					  <span style='background-color:#eaeaea; padding:4px 8px; border-radius:4px; font-weight:bold;'>{tempPassword}</span>
					</p>

					<p style='font-size:15px; color:#555;'>
					  Please log in to the system and change your password as soon as possible to ensure the security of your account.
					</p>

					<div style='text-align:center; margin:24px 0;'>
					  <a href='{config["Authentication:Audience"]}' 
						 style='background-color:#1F8A47; color:#fff; padding:12px 24px; text-decoration:none; border-radius:6px; display:inline-block; font-size:16px;'>
						 Go to Login
					  </a>
					</div>

					<hr style='margin:20px 0; border:none; border-top:1px solid #ddd;' />

					<p style='font-size:14px; color:#777;'>
					  Best regards,<br>
					  <strong>CS-KMS Team</strong>
					</p>
				  </div>
				";
	}
}
