using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
	public interface IPostService
	{
		Task<ResponseKms> CreatePostAsync(PostCreationDTO postCreationDTO);
		Task<ResponseKms> UpdatePostAsync(PostUpdateDTO postUpdateDTO);
		Task<ResponseKms> DeletePostAsync(int id);
		Task<IEnumerable<PostDTO>> GetAllPostsAsync();
		Task<PostDTO?> GetPostAsync(int id);
		Task<PostAttachment?> GetAttachmentAsync(int postId, int  attachmentId); 
		// Task<PostDTO?> GetPostBySearchAsync(string search);
	}
}
