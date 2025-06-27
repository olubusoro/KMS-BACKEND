using CsKmsBackend.Domain.Models;

namespace CsKmsBackend.Application.DTOs.Conversions
{
	public static class LogConversions
	{
		public static LogDTO ToDTO(this Log log) => new(
			log.Id,
			log.Action,
			log.UserName,
			log.EntityType,
			log.PostTitle,
			log.Timestamp);

		public static IEnumerable<LogDTO> ToDTO(this IEnumerable<Log> logs)
		{
			var logDTOs = new List<LogDTO>();
			foreach (var log in logs)
			{
				logDTOs.Add(log.ToDTO());
			}
			return logDTOs;
		}
	}
}
