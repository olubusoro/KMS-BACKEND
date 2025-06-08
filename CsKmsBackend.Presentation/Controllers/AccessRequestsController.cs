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
	public class AccessRequestsController(IAccessRequestService accessRequestService, ICurrentUserService currentUser) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<ResponseKms>> RequestAccess(AccessRequestCreationDTO accessRequestCreationDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var response = await accessRequestService.CreateRequestAsync(currentUser.UserId, accessRequestCreationDTO);
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
			var response = await accessRequestService.DeleteRequestAsync(currentUser.UserId, id);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

		[HttpPatch("{id:int}/approve")]
		public async Task<ActionResult<ResponseKms>> ApproveRequest(int id)
		{
			var response = await accessRequestService.ApproveRequestAsync(currentUser.UserId, id);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

		[HttpPatch("{id:int}/deny")]
		public async Task<ActionResult<ResponseKms>> DenyRequest(int id)
		{
			var response = await accessRequestService.DenyRequestAsync(currentUser.UserId, id);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

	}
}
