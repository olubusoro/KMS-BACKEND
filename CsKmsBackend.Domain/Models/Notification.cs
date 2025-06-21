using System.ComponentModel.DataAnnotations;

namespace CsKmsBackend.Domain.Models
{
	public class Notification
	{
		public int Id { get; set; }
		[Required] public int UserId { get; set; } 
		public User User { get; set; }
		[Required] public string Title { get; set; }
		public string Message { get; set; }
		public bool IsRead { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}

}
