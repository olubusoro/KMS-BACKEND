

using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs
{
    public record CategoryDTO
   (
        int Id,
        [Required] string Name,
        string? Description,
        [Required] int DepartmentId
    );

    
}
