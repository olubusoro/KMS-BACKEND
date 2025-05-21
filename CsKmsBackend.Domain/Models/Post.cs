namespace CsKmsBackend.Domain.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public string Content { get; set; }
		public string Visibility { get; set; }
		public int? CategoryId { get; set; } // int? because no category yet
		//public Category Category { get; set; }
		public int? UserId { get; set; }
		public User CreatedBy { get; set; }
		public IList<PostAttachment> Attachments { get; set; } = [];
		//public IList<AccessRequest> AccessRequests { get; set; } = new List<AccessRequest>();
		//public IList<Log> Logs { get; set; } = new List<Log>();
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
	}
}
