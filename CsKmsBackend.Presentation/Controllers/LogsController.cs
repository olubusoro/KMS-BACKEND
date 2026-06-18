using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	// [DEMO] Original: [Authorize(Roles = "SuperAdmin")]
	[Authorize]
	public class LogsController(IAuditLoggerService auditLogger) : ControllerBase
	{
		[HttpGet]
		
		public async Task<ActionResult<IEnumerable<LogDTO>>> GetLogs()
		{
			var logs = await auditLogger.GetAllLogsAsync();
			return Ok(logs);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<LogDTO>> GetLog(int id)
		{
			var log = await auditLogger.FindLogByIdAsync(id);
			return log is not null ? Ok(log) : NotFound();
		}

	}
}
