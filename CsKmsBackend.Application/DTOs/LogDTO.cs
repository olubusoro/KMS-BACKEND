namespace CsKmsBackend.Application.DTOs
{
	public record LogDTO(
					  int Id,
					  string Action,
					  int PerformedBy,
					  string EntityType,
					  int EntityId,
					  DateTime Timestamp);
}
