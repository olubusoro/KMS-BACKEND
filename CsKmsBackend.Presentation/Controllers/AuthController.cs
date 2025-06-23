using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class AuthController(IAuthService authService) : ControllerBase
	{
		[HttpPost("login")]
		public async Task<ActionResult<ResponseKms>> Login(LoginDTO request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var result = await authService.LoginAsync(request);

			return result.Flag is true ? Ok(result) : BadRequest(result);
		}
	}
}
