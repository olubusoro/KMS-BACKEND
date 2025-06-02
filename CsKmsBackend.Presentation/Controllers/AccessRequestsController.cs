using CsKmsBackend.Application.DTOs.AccessRequests;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sprache;
using System.Security.Claims;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/access-requests")]
	[ApiController]
	public class AccessRequestsController(IAccessRequestService accessRequestService) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<ResponseKms>> RequestAccess(AccessRequestCreationDTO accessRequestCreationDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			_ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
			var response = await accessRequestService.CreateRequestAsync(userId, accessRequestCreationDTO);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AccessRequestDTO>>> GetAccessRequests()
		{
			var accessRequests = await accessRequestService.GetAllRequestAsync();
			return Ok(accessRequests);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<AccessRequestDTO>> GetAccessRequest(int id)
		{
			var accessRequest = await accessRequestService.GetRequestAsync(id);
			return accessRequest is not null ? Ok(accessRequest) : NotFound();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ResponseKms>> CancelAccessRequest(int id)
		{
			_ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
			var response = await accessRequestService.DeleteRequestAsync(userId, id);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

		[HttpPatch("{id:int}/approve")]
		public async Task<ActionResult<ResponseKms>> ApproveRequest(int id)
		{
			_ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
			var response = await accessRequestService.ApproveRequestAsync(userId, id);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

		[HttpPatch("{id:int}/deny")]
		public async Task<ActionResult<ResponseKms>> DenyRequest(int id)
		{
			_ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
			var response = await accessRequestService.DenyRequestAsync(userId, id);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

	}
}
