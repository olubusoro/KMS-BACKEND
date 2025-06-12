using System;
using System.Collections.Generic;
using CsKmsBackend.Domain.Models;
namespace CsKmsBackend.Application.DTOs.Conversions
{
    public static class CategoryConversions
    {
        public static Category ToEntity(this CategoryDTO categoryDTO) => new Category
        {
            Id = categoryDTO.Id,
            Name = categoryDTO.Name,
            Description = categoryDTO.Description,
            DepartmentId = categoryDTO.DepartmentId,
        };

        
        public static CategoryDTO ToDTO(this Category category) => new CategoryDTO
        (
            category.Id,
            category.Name,
            category.Description,
			category.DepartmentId
			);

        public static IEnumerable<CategoryDTO> ToDTO(this IEnumerable<Category> categories)
        {
            var categoryDTOs = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoryDTOs.Add(ToDTO(category));
            }
            return categoryDTOs;
        }
    }

}
