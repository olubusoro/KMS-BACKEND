using CsKmsBackend.Application.DTOs.UserDTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController(IUserService userService, ICurrentUserService currentUser) : ControllerBase
	{
		// POST: api/users
		[HttpPost][AllowAnonymous]
		//[Authorize(Roles = "SuperAdmin")]
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
		[Authorize(Roles = "SuperAdmin")]
		public async Task<ActionResult<IEnumerable<UserListDTO>>> GetAllUsers()
		{
			var users = await userService.GetAllUsersAsync();
			return Ok(users);
		}

		// GET: api/users/5
		[HttpGet("{id:int}")]
		[Authorize(Roles = "SuperAdmin")]
		public async Task<ActionResult<UserDTO>> GetUserById(int id)
		{
			var user = await userService.GetUserByIdAsync(id);
			return user is not null ? Ok(user) : NotFound();
		}

		// GET: api/users/profile
		[HttpGet("profile")]
		[Authorize]
		public async Task<ActionResult<UserDTO>> GetUserProfile()
		{
			var user = await userService.GetUserByIdAsync(currentUser.UserId);
			return user is not null ? Ok(user) : NotFound();
		}

		// GET: api/users/role
		[HttpGet("role")]
		[Authorize]
		public async Task<ActionResult<ResponseKms>> GetUserRole()
		{
			var response = await userService.GetUserRoleByIdAsync(currentUser.UserId);
			return Ok(response);
		}
		

		// PUT: api/users
		[HttpPut]
		[Authorize]
        public async Task<ActionResult<ResponseKms>> UpdateUser(UserUpdateDTO userDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var result = await userService.UpdateUserAsync(userDTO);
			return result.Flag is true ? Ok(result) : BadRequest(result);
		}

		// Delete: api/users/5
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "SuperAdmin")]
		public async Task<ActionResult<ResponseKms>> DeleteUser(int id)
		{
			var result = await userService.DeleteUserAsync(id);
			return result.Flag is true ? Ok(result) : NotFound(result);
		}

		// PATCH or PUT: api/users/5/reset-password
		
		[HttpPatch("{id:int}/reset-password")]
		[Authorize(Roles = "SuperAdmin")]
		public async Task<ActionResult<ResponseKms>> ResetPassword(int id)
		{
			var result = await userService.ResetUserPasswordAsync(id);

			return result.Flag ? Ok(result) : BadRequest(result);
		}

		[HttpPatch("{id:int}/change-password")]
		[Authorize]
		public async Task<ActionResult<ResponseKms>> ChangePassword(int id, ChangePasswordDTO changePasswordDTO)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var result = await userService.ChangeUserPasswordAsync(id, changePasswordDTO);

			return result.Flag ? Ok(result) : BadRequest(result);
		}
	}
}
