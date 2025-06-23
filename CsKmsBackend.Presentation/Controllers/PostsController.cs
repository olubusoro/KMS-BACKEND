using CsKmsBackend.Application.DTOs.PostDTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PostsController(IPostService postService, ICurrentUserService currentUser) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<ResponseKms>> CreatePost([FromForm]PostCreationDTO postCreationDTO)
		{
			if(!ModelState.IsValid) 
				return BadRequest(ModelState);
			var result = await postService.CreatePostAsync(currentUser.UserId, postCreationDTO);
			return result.Flag ? Ok(result) : BadRequest(result);
		} 

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PostListDTO>>> GetAllPosts([FromQuery] string search = null)
		{
			if (!string.IsNullOrEmpty(search))
				return Ok(await postService.GetPostBySearchAsync(search));
			return Ok(await postService.GetAllPostsAsync());
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PostAccessResponse>> GetPostById(int id)
		{
			var response = await postService.GetPostWithAccessAsync(id, currentUser.UserId);
			if (response == null)
				return NotFound();

			return Ok(response);
		}

		[HttpGet("{postId:int}/attachments/{attachmentId:int}")]
		public async Task<IActionResult> DownloadAttachment(int postId, int attachmentId)
		{
			var attachment = await postService.GetAttachmentAsync(postId, attachmentId);
			if(attachment is null)
				return NotFound();
			var bytes = await System.IO.File.ReadAllBytesAsync(attachment.FilePath);
			return File(bytes, attachment.ContentType, attachment.OriginalFileName);
		}

		[HttpPut]
		public async Task<ActionResult<ResponseKms>> UpdatePost(PostUpdateDTO postUpdateDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var result = await postService.UpdatePostAsync(currentUser.UserId, postUpdateDTO);
			return result.Flag ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ResponseKms>> DeletePost(int id)
		{
			var result = await postService.DeletePostAsync(currentUser.UserId, id);
			return result.Flag ? Ok(result) : BadRequest(result);
		}

	}
}
