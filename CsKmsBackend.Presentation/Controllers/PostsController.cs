using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController(IPostService postService) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<ResponseKms>> CreatePost([FromForm]PostCreationDTO postCreationDTO)
		{
			if(!ModelState.IsValid) 
				return BadRequest(ModelState);
			_ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
			var result = await postService.CreatePostAsync(userId, postCreationDTO);
			return result.Flag ? Ok(result) : BadRequest(result);
		} 

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts([FromQuery] string search = null)
		{
			if (!string.IsNullOrEmpty(search))
				return Ok(await postService.GetPostBySearchAsync(search));
			return Ok(await postService.GetAllPostsAsync());
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PostDTO>> GetPost(int id)
		{
			var post = await postService.GetPostAsync(id);
			return post is not null ? Ok(post) : NotFound();
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
			_ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
			var result = await postService.UpdatePostAsync(userId, postUpdateDTO);
			return result.Flag ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ResponseKms>> DeletePost(int id)
		{
			_ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);
			var result = await postService.DeletePostAsync(userId, id);
			return result.Flag ? Ok(result) : BadRequest(result);
		}

	}
}
