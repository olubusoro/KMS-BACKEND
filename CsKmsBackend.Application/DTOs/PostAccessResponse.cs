using CsKmsBackend.Application.DTOs.PostDTOs;
using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs
{
    public class PostAccessResponse
	{
		public bool AccessGranted { get; set; }
		public PostDTO? Post { get; set; }
		public string? AccessRequestStatus { get; set; } // "Approved", "Pending", "Denied", "NotRequested"
		public string? Title { get; set; }
	}
}
