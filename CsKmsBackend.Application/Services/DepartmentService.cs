using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.DTOs.DepartmentDTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Application.Interfaces.RepoInterfaces;
using CsKmsBackend.Domain.Models;




namespace CsKmsBackend.Application.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepo, ICategoryRepository categoryRepo) : IDepartmentService
    {
        public async Task<ResponseKms> CreateDepartmentAsync(CreateDepartmentDTO DepartmentDTO)
        {
            var department = DepartmentDTO.ToEntity();
            var response = await departmentRepo.CreateAsync(department);
			if (response.Flag)
				await categoryRepo.CreateAsync(new Category()
				{
					Name = "General",
					Description = "A general category for miscellaneous knowledge posts",
					Department = department
				});
            return response;
        }
        public async Task<ResponseKms> DeleteDepartmentAsync(int id)
		{
			return await departmentRepo.DeleteAsync(id);;
		}

		public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
		{
			var departments = await departmentRepo.GetAllAsync();
            return departments.ToDTO();
        }

        public async Task<DepartmentDTO?> GetDepartmentByIdAsync(int id)
		{
			var department = await departmentRepo.GetByAsync(d=>d.Id == id);
			return department is not null ? department.ToDTO() : null;
		}

        public async Task<ResponseKms> UpdateDepartmentAsync(DepartmentUpdateDTO departmentDTO)
		{
			var department = departmentDTO.ToEntity();
			return await departmentRepo.UpdateAsync(department);
		}
    }
}