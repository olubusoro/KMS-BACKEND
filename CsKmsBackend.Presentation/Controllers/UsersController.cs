using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles ="SuperAdmin")]
	public class UsersController(IUserService userService) : ControllerBase
	{
		// POST: api/users
		[HttpPost]
		public async Task<ActionResult<ResponseKms>> CreateUser(UserCreationDTO userCreationDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			if (!Enum.IsDefined(typeof(UserRole), userCreationDTO.Role.ToString()))
			{
				return BadRequest("Invalid user role.");
			}

			var result = await userService.CreateUserAsync(userCreationDTO);

			return result.Flag is true ? Ok(result) : BadRequest(result);
		}

		// GET: api/users
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
		{
			var users = await userService.GetAllUsersAsync();
			return Ok(users);
		}

		// GET: api/users/5
		[HttpGet("{id:int}")]
		public async Task<ActionResult<UserDTO>> GetUserById(int id)
		{
			var user = await userService.GetUserByIdAsync(id);
			return user is not null ? Ok(user) : NotFound();
		}

		// PUT: api/users
		[HttpPut]
		public async Task<ActionResult<ResponseKms>> UpdateUser(UserDTO userDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var result = await userService.UpdateUserAsync(userDTO);
			return result.Flag is true ? Ok(result) : BadRequest(result);
		}

		// Delete: api/users/5
		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ResponseKms>> DeleteUser(int id)
		{
			var result = await userService.DeleteUserAsync(id);
			return result.Flag is true ? Ok(result) : NotFound(result);
		}

		// PATCH or PUT: api/users/5/reset-password
	}
}
