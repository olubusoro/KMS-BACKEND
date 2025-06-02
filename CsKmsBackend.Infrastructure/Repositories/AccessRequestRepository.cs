using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CsKmsBackend.Infrastructure.Repositories
{
    public class AccessRequestRepository(KmsDbContext context) : IAccessRequestRepository
	{
		public async Task<ResponseKms> CreateAsync(AccessRequest entity)
		{
			try
			{
				var getRequest = await context.AccessRequests.Where(r=> r.UserId.Equals(entity.UserId) 
				&& r.PostId.Equals(entity.PostId)).FirstOrDefaultAsync();
				if(getRequest is not null)
					return new ResponseKms(false, "Access to post already requested by user");
				var request = context.AccessRequests.Add(entity).Entity;
				await context.SaveChangesAsync();
				return request.Id > 0 ? new ResponseKms(true, $"access requested for post with id {request.PostId}")
					: new ResponseKms(false, "Failed to request access");
			}
			catch
			{
				return new ResponseKms(false, "Error occurred while trying to request access. Check UserId or PostId");
			}
		}

		public async Task<ResponseKms> DeleteAsync(int id)
		{
			try
			{
				var request = await FindByIdAsync(id);
				if (request is null) return new ResponseKms(false, "Access request not found");
				context.AccessRequests.Remove(request);
				await context.SaveChangesAsync();
				return new ResponseKms(true, $" Deleted access request with id {id} successfully");
			}
			catch
			{
				return new ResponseKms(false, "Error occured while trying to delete access request");
			}
		}

		public async Task<AccessRequest?> FindByIdAsync(int id)
		{
			try
			{
				var request = await context.AccessRequests.FindAsync(id);
				if (request is null) return null;
				return request;
			}
			catch
			{
				return null;
			}
		}

		public async Task<IEnumerable<AccessRequest>> GetAllAsync()
		{
			try
			{
				var requests = await context.AccessRequests.AsNoTracking().ToListAsync();
				return requests.Count > 0 ? requests : [];
			}
			catch
			{
				return [];
			}
		}

		public async Task<AccessRequest?> GetByAsync(Expression<Func<AccessRequest, bool>> predicate)
		{
			try
			{
				var request = await context.AccessRequests.FirstOrDefaultAsync(predicate);
				return request is not null && request.Id > 0 ? request : null;
			}
			catch
			{
				return null;
			}
		}

		public async Task<ResponseKms> UpdateAsync(AccessRequest entity)
		{
			try
			{
				var getRequest = await FindByIdAsync(entity.Id);
				if (getRequest is null) return new ResponseKms(false, "Access request not found");
				MapUpdate(getRequest,entity);
				await context.SaveChangesAsync();
				return new ResponseKms(true, "Updated Access Request successfully");
			}catch {
				return new ResponseKms(false, "Error occurred while trying to update access request");
			}
		}

		private static void MapUpdate(AccessRequest dbRequest, AccessRequest entity)
		{
			if(!entity.Status.Equals(0))
				dbRequest.Status = entity.Status;
			if(!string.IsNullOrEmpty(entity.Reason))
				dbRequest.Reason = entity.Reason;
		}

		public async Task<ResponseKms> ApproveAsync(int id)
		{
			try { 
				var request = await FindByIdAsync(id);
				if (request is null) return new ResponseKms(false, $"Access request with id {id} not found");
				else if (request.Status != Domain.Models.Enums.Status.Pending)
					return new ResponseKms(false, "Access request already reviewed");
				request.Status = Domain.Models.Enums.Status.Approved;
				await context.SaveChangesAsync();
				return new ResponseKms(true, "Access request approved successfully");
			}
			catch
			{
				return new ResponseKms(false, "Error occurred while approving access request");
			}
		}

		public async Task<ResponseKms> DenyAsync(int id)
		{
			try
			{
				var request = await FindByIdAsync(id);
				if (request is null) return new ResponseKms(false, $"Request with id {id} not found");
				else if (request.Status != Domain.Models.Enums.Status.Pending)
					return new ResponseKms(false, "Access request already reviewed");
				request.Status = Domain.Models.Enums.Status.Denied;
				await context.SaveChangesAsync();
				return new ResponseKms(true, "Access request denied successfully");
			}
			catch
			{
				return new ResponseKms(false, "Error occurred while trying to deny access request");
			}
		}
	}
}
