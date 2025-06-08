using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace CsKmsBackend.Application.Services
{
    public class PostService(IPostRepository postRepo, IAuditLoggerService logger) : IPostService
	{
		public async Task<ResponseKms> CreatePostAsync(int userId, PostCreationDTO postCreationDTO)
		{
			var allowedExtensions = new[] { ".pdf", ".docx", ".txt" };
			var maxSize = 20 * 1024 * 1024; //20mb

			var post = postCreationDTO.ToEntity();
			post.UserId = userId;

			var uploadRoot = Path.Combine($"Uploads/Attachments/Departments/Category");
			if(!Directory.Exists(uploadRoot))
				Directory.CreateDirectory(uploadRoot);

			if(postCreationDTO.Attachments is not null) { 
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
			}

			var result = await postRepo.CreateAsync(post);
			if (result.Flag)
			{
				await logger.LogCreateAsync(Domain.Models.Enums.ActionType.Create, userId, Domain.Models.Enums.EntityType.Post);
			}
			return result;
		}

		public async Task<ResponseKms> DeletePostAsync(int userId, int id)
		{
			var post = await postRepo.GetByAsync(p=> p.Id==id);
			var filePaths = post.Attachments.Select(p => p.FilePath).ToList();
			var result = await postRepo.DeleteAsync(id);
			if (result.Flag)
			{
				await logger.LogAsync(Domain.Models.Enums.ActionType.Delete, userId, Domain.Models.Enums.EntityType.Post, post.Id);
				if (filePaths.Count > 0)
				{
					try
					{
						foreach (var filePath in filePaths)
						{
							File.Delete(filePath);
						}
					}
					catch
					{
						return new ResponseKms(true, "deleted post but failed to delete associated file(s)");
					}
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

		public async Task<ResponseKms> UpdatePostAsync(int userId, PostUpdateDTO postUpdateDTO)
		{
			var post = postUpdateDTO.ToEntity();
			var result = await postRepo.UpdateAsync(post);
			if (result.Flag)
			{
				await logger.LogAsync(Domain.Models.Enums.ActionType.Update, userId, Domain.Models.Enums.EntityType.Post, post.Id);
			}
			return result;
		}

		public async Task<IEnumerable<PostDTO>> GetPostBySearchAsync(string search)
		{
			var posts = await postRepo.SearchAsync(search);
			return posts.ToDTO();
		}
	}
}
