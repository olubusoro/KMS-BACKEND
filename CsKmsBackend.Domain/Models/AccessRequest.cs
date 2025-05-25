using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Domain.Models
{
	public class AccessRequest
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public Post RequestedPost { get; set; }
		public int UserId { get; set; }
		public User RequestedBy { get; set; }
		public string Reason { get; set; } = string.Empty;
		public Status Status { get; set; }
		public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

	}
}
