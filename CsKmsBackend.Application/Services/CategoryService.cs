

using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Services
{
    public class CategoryService(ICategoryRepository CategoryRepo) : ICategoryService
    {

        public async Task<ResponseKms> CreateCategoryAsync(CategoryDTO CategoryDTO)
        {
          var category = CategoryDTO.ToEntity();
            var result = await CategoryRepo.CreateAsync(category);
            return result;
        }

        public async Task<ResponseKms> DeleteCategoryAsync(int id)
        {
            var result = await CategoryRepo.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync(int userId)
        {
            var categories = await CategoryRepo.GetAllByUserIdAsync(userId);
            return categories.Any() ? categories.ToDTO() : [];
        }

        public async Task<CategoryDTO?> GetCategoryAsync(int id)
        {
            var category = await CategoryRepo.FindByIdAsync(id);
            return category?.ToDTO();
        }

        public async Task<ResponseKms> UpdateCategoryAsync(CategoryDTO CategoryUpdateDTO)
        {
         var category = CategoryUpdateDTO.ToEntity();
            var result = await CategoryRepo.UpdateAsync(category);
            return result;
        }
    }
}
