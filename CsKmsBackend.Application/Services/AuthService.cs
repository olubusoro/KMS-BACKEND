using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CsKmsBackend.Application.Services
{
	public class AuthService(IUserRepository userRepo, IConfiguration config) : IAuthService
	{
		public async Task<ResponseKms> LoginAsync(LoginDTO request)
		{
			var user = await userRepo.GetByAsync(u => u.Email.Equals(request.Email));
			if (user is null) 
				return new ResponseKms(false, "Invalid Credentials");
			var IsVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
			if(!IsVerified)
				return new ResponseKms(false, "Invalid Credentials");

			string token = GenerateJwtToken(user);
			return new ResponseKms(true,token);

		}

		private string GenerateJwtToken(User user)
		{
			var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
			var securityKey = new SymmetricSecurityKey(key);
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier,user.Id.ToString()),
				// new(ClaimTypes.Name, user.Name!),
				new(ClaimTypes.Email, user.Email!),
				new(ClaimTypes.Role, user.Role.ToString())
			};

			var token = new JwtSecurityToken(
				issuer: config["Authentication:Issuer"],
				audience: config["Authentication:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(config.GetValue<int>("Authentication:TokenLifeTimeMinutes")),
				signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
