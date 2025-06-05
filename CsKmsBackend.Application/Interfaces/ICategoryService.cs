
using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
    public interface ICategoryService
    {
        //add createasync category and the rest 
        Task<ResponseKms> CreateCategoryAsync(CategoryDTO CategoryDTO);
        Task<ResponseKms> DeleteCategoryAsync(int id);
        Task<ResponseKms> UpdateCategoryAsync(CategoryDTO CategoryUpdateDTO);

        Task<CategoryDTO?> GetCategoryAsync(int id);
        Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync();

    }
}
