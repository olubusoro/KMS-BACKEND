using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.DTOs.DepartmentDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<ResponseKms> CreateDepartmentAsync(CreateDepartmentDTO DepartmentDTO);
        Task<ResponseKms> UpdateDepartmentAsync(DepartmentUpdateDTO DepartmentDTO);
		Task<ResponseKms> DeleteDepartmentAsync(int id);
		Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
		Task<DepartmentDTO?> GetDepartmentByIdAsync(int id);
    }
}