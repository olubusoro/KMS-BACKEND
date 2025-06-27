using CsKmsBackend.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Domain.Models
{
	public class Feedback
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public User User { get; set; }
		[MaxLength(100)] public string Subject { get; set; }
		[MaxLength(1500)] public string Message { get; set; }
		public FeedbackType Type { get; set; } = FeedbackType.General;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		
	}
}
