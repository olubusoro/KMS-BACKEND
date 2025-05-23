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
			if(postCreationDTO.UserId < 1)
				return new ResponseKms(false, "invalid user id");
			var allowedExtensions = new[] { ".pdf", ".docx", ".txt" };
			var maxSize = 20 * 1024 * 1024; //20mb

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
			var post = await postRepo.GetByAsync(p=> p.Id==id);
			var filePaths = post.Attachments.Select(p => p.FilePath).ToList();
			var result = await postRepo.DeleteAsync(id);
			if (result.Flag)
			{
				try
				{
					foreach(var filePath in filePaths)
					{
						File.Delete(filePath);
					}
				}
				catch
				{
					return new ResponseKms(true, "deleted post but failed to delete associated file(s)");
				}
			}
			return result;
		}

		public async Task<PostAttachment?> GetAttachmentAsync(int postId, int attachmentId)
		{
			var post = await postRepo.GetByAsync(p => p.Id == postId);
			if (post is null) 
				return null;
			var attachment = post.Attachments.FirstOrDefault(a=>a.Id == attachmentId);
			if (attachment is null || !File.Exists(attachment.FilePath))
				return null;

			return attachment;
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
