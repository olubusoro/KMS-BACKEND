using System.Linq.Expressions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CsKmsBackend.Infrastructure.Repositories
{
    public class CategoryRepository(KmsDbContext context) : ICategoryRepository
    {
        public async Task<ResponseKms> CreateAsync(Category entity)
        {

            try
            {
                var category = context.Categories.Add(entity).Entity;
                await context.SaveChangesAsync();

              
                return category is not null && category.Id > 0 ? new ResponseKms(true, "category created successfully")
                    : new ResponseKms(false, "Error occured while creating category");
            } catch(Exception ex)
            {
                 return new ResponseKms(false, ex.Message);
            }
        }
        

        public async Task<ResponseKms> DeleteAsync(int id)
        {
            try
            {
                var getCategory = await FindByIdAsync(id);
                if (getCategory is null)
                    return new ResponseKms(false, "Category does not exist");

                context.Categories.Remove(getCategory);
                await context.SaveChangesAsync();
                return new ResponseKms(true, "Category deleted succesfully");
            }
            catch
            {
                return new ResponseKms(false, "Error occured while trying to delete category"); ;
            }
        }

        public async Task<Category?> FindByIdAsync(int id)
        {
           try
            {
                var category = await context.Categories.FindAsync(id);
                if (category is null)
                    return null;
                return category;
            }catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
          try
            {
                var category = await context.Categories.AsNoTracking()
                    .ToListAsync();
                return category.Count != 0 ? category : [];
            } catch
            {
                return [];
            }

        }

        public async Task<Category?> GetByAsync(Expression<Func<Category, bool>> predicate)
        {
            try
            {
                var category = await context.Categories.Where(predicate).FirstOrDefaultAsync();
                    
                return category is not null && category.Id > 0 ? category : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseKms> UpdateAsync(Category entity)
        {
            try
            {
                var getCategory = await FindByIdAsync(entity.Id);
                if (getCategory is null)
                    return new ResponseKms(false, "Category does not exist");

                MapUpdate(getCategory, entity);
                await context.SaveChangesAsync();
                return new ResponseKms(true, "Category updated successfully");
            }
            catch
            {
                return new ResponseKms(false, "Error occurred while updating Category");
            }
        }

        private static void MapUpdate(Category originalCategory, Category CategoryUpdate)
        {
            originalCategory.Description = CategoryUpdate.Description ?? originalCategory.Description;
            originalCategory.Name = CategoryUpdate.Name ?? originalCategory.Name;
          
        }
    }
}
