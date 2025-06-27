namespace CsKmsBackend.Application.DTOs
{
	public record LogDTO(
					  int Id,
					  string Action,
					  string PerformedBy,
					  string EntityType,
					  string PostTitle,
					  DateTime Timestamp);
}
