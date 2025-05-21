using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using System.Net.Mail;

namespace CsKmsBackend.Application.Services
{
	public class PostService(IPostRepository postRepo) : IPostService
	{
		public async Task<ResponseKms> CreatePostAsync(PostCreationDTO postCreationDTO)
		{
			var allowedExtensions = new[] { ".pdf", ".docx", ".txt" };
			var maxSize = 20 * 1024 * 1024;

			var post = postCreationDTO.ToEntity();

			var uploadRoot = Path.Combine($"Uploads/Attachments/Departments/Category");
			if(!Directory.Exists(uploadRoot))
				Directory.CreateDirectory(uploadRoot);

            foreach (var file in postCreationDTO.Attachments)
            {
				var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
				if (!allowedExtensions.Contains(ext))
					return new ResponseKms(false, $"Unsupported file type: {ext}");

				if (file.Length > maxSize)
					return new ResponseKms(false, "File too large");

				var safeFileName = Path.GetRandomFileName() + ext;
				var fullPath = Path.Combine(uploadRoot, safeFileName);

				using var stream = new FileStream(fullPath, FileMode.Create);
				await file.CopyToAsync(stream);

				post.Attachments.Add(new PostAttachment
				{
					OriginalFileName = file.FileName,
					FileName = safeFileName,
					FilePath = fullPath,
					ContentType = file.ContentType
				});
			}

            var result = await postRepo.CreateAsync(post);
			return result;
		}

		public async Task<ResponseKms> DeletePostAsync(int id)
		{
			var result = await postRepo.DeleteAsync(id);
			return result;
		}

		public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
		{
			var posts = await postRepo.GetAllAsync();
			return posts.ToDTO();
		}

		public async Task<PostDTO?> GetPostAsync(int id)
		{
			var post = await postRepo.GetByAsync(p => p.Id == id);
			return post is not null ? post.ToDTO() : null;
		}

		public Task<ResponseKms> UpdatePostAsync(PostUpdateDTO postUpdateDTO)
		{
			var post = postUpdateDTO.ToEntity();
			var result = postRepo.UpdateAsync(post);
			return result;
		}
	}
}
