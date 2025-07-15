using CsKmsBackend.Application.DTOs.DepartmentDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs.Conversions
{
    public static class DepartmentConversions
    {
        public static Department ToEntity(this DepartmentUpdateDTO DepartmentDTO) => new Department
        {
            Id = DepartmentDTO.Id,
			Name = DepartmentDTO.Name,
            Description = DepartmentDTO.Description
        };
        
        public static Department ToEntity(this CreateDepartmentDTO DepartmentDTO) => new Department
        {
			Name = DepartmentDTO.Name,
            Description = DepartmentDTO.Description
        };

        public static DepartmentDTO ToDTO(this Department department) => new DepartmentDTO(department.Id,
                        department.Name,
                        department.Description,
                        department.Categories.ToDTO().ToList());
        
        public static IEnumerable<DepartmentDTO> ToDTO(this IEnumerable<Department> departments)
        {
            var departmentDTOs = new List<DepartmentDTO>();
            foreach (var department in departments)
            {
                departmentDTOs.Add(ToDTO(department));
            }
            return departmentDTOs;
        }
    }
}