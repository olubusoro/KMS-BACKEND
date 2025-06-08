using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Domain.Models
{
	public class Log
	{
		public int Id { get; set; }
		public string Action { get; set; }
		public int PerformedBy { get; set; }
		public string EntityType { get; set; }
		public int EntityId { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;

	}
	
}
