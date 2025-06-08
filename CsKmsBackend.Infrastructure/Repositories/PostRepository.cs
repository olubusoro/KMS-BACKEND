using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CsKmsBackend.Infrastructure.Repositories
{
    public class PostRepository(KmsDbContext context) : IPostRepository
	{
		public async Task<ResponseKms> CreateAsync(Post entity)
		{
			try
			{
				var getPost = await GetByAsync(p => p.Title == entity.Title);
				if (getPost is not null)
					return new ResponseKms(false, "This title already exists.");
				
				var post = context.Posts.Add(entity).Entity;
				await context.SaveChangesAsync();
				return post.Id > 0 && post is not null ? new ResponseKms(true, "Knowledge post created successfully")
					: new ResponseKms(false, "Error occured while trying to create knowledge post");
			}
			catch(Exception e) { 
				return new ResponseKms(false, e.Message);
			}
		}

		public async Task<ResponseKms> DeleteAsync(int id)
		{
			try
			{
				var getPost = await FindByIdAsync(id);
				if (getPost is null)
					return new ResponseKms(false, "Post does not exist");

				context.Posts.Remove(getPost);
				await context.SaveChangesAsync();
				return new ResponseKms(true, "Post deleted successfully");
			}
			catch
			{
				return new ResponseKms(false, "Error occured while trying to delete post");
			}
		}

		public async Task<Post?> FindByIdAsync(int id)
		{
			try
			{
				var post = await context.Posts.FindAsync(id);
				if (post is null)
					return null;
				return post;
			}
			catch
			{
				return null;
			}
		}

		public async Task<IEnumerable<Post>> GetAllAsync()
		{
			try
			{
				var posts = await context.Posts.AsNoTracking()
					.Include(p=>p.Attachments)
					.Include(p=>p.CreatedBy).ToListAsync();
				return posts.Count != 0 ? posts : Enumerable.Empty<Post>();
			}
			catch
			{
				return [];
			}
		}

		public async Task<Post?> GetByAsync(Expression<Func<Post, bool>> predicate)
		{
			try
			{
				var post = await context.Posts.Where(predicate)
					.Include(p=>p.CreatedBy).Include(p=>p.Attachments).FirstOrDefaultAsync();
				return post is not null && post.Id > 0 ? post : null;
			}
			catch
			{
				return null;
			}
		}

		public async Task<IEnumerable<Post>> SearchAsync(string search)
		{
			try
			{
				var posts = await context.Posts.Where(
					p => p.Title.Contains(search) 
					|| (p.Description != null && p.Description.Contains(search)))
					.Include(p=>p.CreatedBy)
					.Include(p=>p.Attachments)
					.ToListAsync();
				return posts.Count != 0 ? posts : [];
			}catch 
			{
				return [];
			}
		}

		public async Task<ResponseKms> UpdateAsync(Post entity)
		{
			try
			{
				var getPost = await FindByIdAsync(entity.Id);
				if (getPost is null)
					return new ResponseKms(false, "Post does not exist");

				MapUpdate(getPost,entity);
				await context.SaveChangesAsync();
				return new ResponseKms(true, "post updated successfully");
			}catch
			{
				return new ResponseKms(false, "Error occurred while updating post");
			}
		}

		private void MapUpdate(Post originalPost, Post postUpdate)
		{
			originalPost.Title = postUpdate.Title ?? originalPost.Title;
			originalPost.Description = postUpdate.Description ?? originalPost.Description;
			originalPost.Content = postUpdate.Content ?? originalPost.Content;
			originalPost.Visibility = postUpdate.Visibility ?? originalPost.Visibility;
			originalPost.UpdatedAt = DateTime.UtcNow;
		}
	}
}
