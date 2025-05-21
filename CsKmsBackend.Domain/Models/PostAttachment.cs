namespace CsKmsBackend.Domain.Models
{
	public class PostAttachment
	{
		public int Id { get; set; }
		public string OriginalFileName { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
		public string FilePath { get; set; }

		public int PostId { get; set; }
		public Post Post { get; set; }
	}
}
