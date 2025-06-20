using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.PostDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
    public interface IPostService
	{
		Task<ResponseKms> CreatePostAsync(int userId, PostCreationDTO postCreationDTO);
		Task<ResponseKms> UpdatePostAsync(int userId, PostUpdateDTO postUpdateDTO);
		Task<ResponseKms> DeletePostAsync(int userId, int id);
		Task<IEnumerable<PostListDTO>> GetAllPostsAsync();
		Task<PostDTO?> GetPostAsync(int id);
		Task<PostAccessResponse?> GetPostWithAccessAsync(int postId, int userId);
		Task<PostAttachment?> GetAttachmentAsync(int postId, int  attachmentId); 
		Task<IEnumerable<PostListDTO>> GetPostBySearchAsync(string search);
	}
}
