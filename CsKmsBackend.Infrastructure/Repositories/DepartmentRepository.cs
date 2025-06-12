using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CsKmsBackend.Infrastructure.Repositories
{
    public class DepartmentRepository(KmsDbContext context) : IDepartmentRepository
    {
        public async Task<ResponseKms> CreateAsync (Department entity)
        {
            try
            {
                var getDepartment = await GetByAsync(u=> u.Name ==  entity.Name);
                if (getDepartment is not null)
                    return new ResponseKms(false, "Department already exists");
                var department = context.Departments.Add(entity).Entity;

                await context.SaveChangesAsync();
                return department is not null && department.Id > 0 ? new ResponseKms(true, "new department created successfully")
                    : new ResponseKms(false, "Error occured while creating new department");    
            }
            catch(Exception ex) {
                return new ResponseKms(false, ex.Message);
            }
        }

        public async Task<ResponseKms> DeleteAsync(int id)
        {
            try
            {
                var department = await FindByIdAsync(id);
                if(department is null)
                    return new ResponseKms(false, $"department with id {id} not found");
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
                return new ResponseKms(true, $"department with id {id} has been deleted successfully");
            }catch(Exception ex)
            {
                return new ResponseKms(false, ex.Message);
            }
        }

        public async Task<Department?> FindByIdAsync(int id)
		{
			try
			{
				var department = await context.Departments.FindAsync(id);
				if(department is null)
					return null;
				return department;
			}catch
			{
				return null;
			}
		}

        public async Task<IEnumerable<Department>> GetAllAsync()
		{
			try
			{
				var departments = await context.Departments.Include(d=>d.Categories).AsNoTracking().ToListAsync();
				return departments.Any() ? departments : Enumerable.Empty<Department>();
			}
			catch
			{
				return [];
			}
		}

        public async Task<Department?> GetByAsync(Expression<Func<Department, bool>> predicate)
		{
			try
			{
				var department = await context.Departments.Where(predicate).Include(d=>d.Categories).FirstOrDefaultAsync();
				return department is not null && department.Id > 0 ? department : null;
			}
			catch
			{
				return null;
			}
		}

        public async Task<ResponseKms> UpdateAsync(Department entity)
		{
			try
			{
				var getDepartment = await FindByIdAsync(entity.Id);
				if (getDepartment is null)
					return new ResponseKms(false, "department not found");

				MapUpdate(getDepartment,entity);
				await context.SaveChangesAsync();
				return new ResponseKms(true, "department updated successfully");
				
			}
			catch
			{
				return new ResponseKms(false, "Error ocurred when updating department, try again.");
			}
		}

		private void MapUpdate(Department originalDepartment, Department departmentUpdate)
		{
			originalDepartment.Name = departmentUpdate.Name ?? originalDepartment.Name;
			originalDepartment.Description = departmentUpdate.Description ?? originalDepartment.Description;
		}
    }
}