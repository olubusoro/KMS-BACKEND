using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController(IDepartmentService departmentService) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<ResponseKms>> CreateDepartment(CreateDepartmentDTO departmentDTO)
		{
			var result = await departmentService.CreateDepartmentAsync(departmentDTO);

			return result.Flag is true ? Ok(result) : BadRequest(result);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
		{
			var departments = await departmentService.GetAllDepartmentsAsync();
			return Ok(departments);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<DepartmentDTO>> GetDepartmentById(int id)
		{
			var department = await departmentService.GetDepartmentByIdAsync(id);
			return department is not null ? Ok(department) : NotFound();
		}
		[HttpPut]
		public async Task<ActionResult<ResponseKms>> UpdateDepartment(DepartmentDTO departmentDTO)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var result = await departmentService.UpdateDepartmentAsync(departmentDTO);
			return result.Flag is true ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ResponseKms>> DeleteDepartment(int id)
		{
			var result = await departmentService.DeleteDepartmentAsync(id);
			return result.Flag is true ? Ok(result) : NotFound(result);
		}
	}
}
