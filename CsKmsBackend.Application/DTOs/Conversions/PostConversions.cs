using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs.Conversions
{
	public static class PostConversions
	{
		public static Post ToEntity(this PostCreationDTO postCreationDTO) => new()
		{
			Title = postCreationDTO.Title,
			Description = postCreationDTO.Description,
			Content = postCreationDTO.Content,
			CategoryId = postCreationDTO.CategoryId,
			Visibility = postCreationDTO.Visibility
		};

		public static Post ToEntity(this PostUpdateDTO postupdateDTO) => new()
		{
			Id = postupdateDTO.Id,
			Title = postupdateDTO.Title,
			Description = postupdateDTO.Description,
			Content = postupdateDTO.Content,
			Visibility = postupdateDTO.Visibility,
		};

		public static PostDTO ToDTO(this Post post) => new(
			post.Id,
			post.Title,
			post.Description,
			post.Content,
			post.Visibility,
			post.CategoryId,
			post.CreatedBy.Name,
			post.Attachments.Select(a=>new PostAttachmentDTO(a.FileName,a.FilePath)).ToList());
		

		public static IEnumerable<PostDTO> ToDTO(this IEnumerable<Post> posts)
		{
			var postDTOs = new List<PostDTO>();
			foreach (var post in posts)
			{
				postDTOs.Add(ToDTO(post));
			}
			return postDTOs;
		}
	}
}
