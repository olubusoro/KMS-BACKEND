using CsKmsBackend.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.UserDTOs
{
    public record UserUpdateDTO(
        int Id,
        [Required] string Name,
        [Required] string Email,
        [Required] UserRole Role,
        [Required, MinLength(1, ErrorMessage = "Please pick one department")] List<int> DepartmentIds

    );
}
