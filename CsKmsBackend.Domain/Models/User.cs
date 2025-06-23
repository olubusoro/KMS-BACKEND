using CsKmsBackend.Domain.Models.Enums;

namespace CsKmsBackend.Domain.Models
{
    public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public UserRole Role { get; set; } = UserRole.Staff;
		public IList<Department> Departments { get; set; } = [];
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
