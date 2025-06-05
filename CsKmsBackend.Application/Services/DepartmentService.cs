using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.Conversions;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;




namespace CsKmsBackend.Application.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepo) : IDepartmentService
    {
        public async Task<ResponseKms> CreateDepartmentAsync(DepartmentDTO DepartmentDTO)
        {
            var department = DepartmentDTO.ToEntity();
            var response = await departmentRepo.CreateAsync(department);
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
			var department = await departmentRepo.FindByIdAsync(id);
			return department is not null ? department.ToDTO() : null;
		}

        public async Task<ResponseKms> UpdateDepartmentAsync(DepartmentDTO departmentDTO)
		{
			var department = departmentDTO.ToEntity();
			return await departmentRepo.UpdateAsync(department);
		}
    }
}