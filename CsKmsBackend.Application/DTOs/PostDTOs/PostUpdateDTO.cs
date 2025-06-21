using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Application.DTOs.PostDTOs
{
    public record PostUpdateDTO(
        [Required] int Id,
        string? Title,
        string? Description,
        string? Content,
        string? Visibility
        );
}
