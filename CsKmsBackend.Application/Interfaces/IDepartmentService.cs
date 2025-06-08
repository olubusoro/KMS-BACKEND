using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<ResponseKms> CreateDepartmentAsync(DepartmentDTO DepartmentDTO);
        Task<ResponseKms> UpdateDepartmentAsync(DepartmentDTO DepartmentDTO);
		Task<ResponseKms> DeleteDepartmentAsync(int id);
		Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
		Task<DepartmentDTO?> GetDepartmentByIdAsync(int id);
    }
}