using CsKmsBackend.Application.DTOs.FeedbackDTOs;
using CsKmsBackend.Application.DTOs.UserDTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedbacksController(IFeedbackService feedbackService, ICurrentUserService currentUser) : ControllerBase
	{
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<ResponseKms>> Submit(FeedbackCreateDTO feedbackCreateDTO)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (!Enum.IsDefined(typeof(FeedbackType), feedbackCreateDTO.Type.ToString()))
				return BadRequest("Invalid Feedback Type.");

			var response = await feedbackService.AddFeedbackAsync(currentUser.UserId, feedbackCreateDTO);
			return response.Flag ? Ok(response) : BadRequest(response);
		}

		[HttpGet]
		[Authorize(Roles = "SuperAdmin")]
		public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetAll()
		{
			var feedbacks = await feedbackService.GetAllFeedbackAsync();
			return Ok(feedbacks);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "SuperAdmin")]
		public async Task<ActionResult<FeedbackDTO>> GetById(int id)
		{
			var feedback = await feedbackService.GetFeedbackByIdAsync(id);
			return feedback is not null ? Ok(feedback) : NotFound();
		}
	}
}
