using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController(IPostService postService) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<ResponseKms>> CreatePost([FromForm]PostCreationDTO postCreationDTO)
		{
			var result = await postService.CreatePostAsync(postCreationDTO);
			return result.Flag ? Ok(result) : BadRequest(result);
		} 

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
		{
			return Ok(await postService.GetAllPostsAsync());
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PostDTO>> GetPost(int id)
		{
			var post = await postService.GetPostAsync(id);
			return post is not null ? Ok(post) : NotFound();
		}

		[HttpPut]
		public async Task<ActionResult<ResponseKms>> UpdatePost(PostUpdateDTO postUpdateDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var result = await postService.UpdatePostAsync(postUpdateDTO);
			return result.Flag ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ResponseKms>> DeletePost(int id)
		{
			var result = await postService.DeletePostAsync(id);
			return result.Flag ? Ok(result) : BadRequest(result);
		}
	}
}
