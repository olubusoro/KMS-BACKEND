using CsKmsBackend.Application.DTOs;
using CsKmsBackend.Application.Interfaces;
using CsKmsBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsKmsBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService, ICurrentUserService currentUser) : ControllerBase
    {
        [HttpGet]
		// [DEMO] Original: [Authorize(Roles = "DeptAdmin")]
		[Authorize]
		public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories() {
            var categories = await categoryService.GetAllCategoryAsync(currentUser.UserId);
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
		[Authorize]
		public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await categoryService.GetCategoryAsync(id);
                return category is not null? Ok(category): NotFound();
        }

        //delete put and post endpoints
        [HttpDelete("{id:int}")]
		// [DEMO] Original: [Authorize(Roles = "DeptAdmin")]
		[Authorize]
		public async Task<ActionResult<ResponseKms>> DeleteCategory(int id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);
            return result.Flag ? Ok(result) : BadRequest(result);
        }


        [HttpPut]
        // [DEMO] Original: [Authorize(Roles = "DeptAdmin")]
        [Authorize]
        public async Task<ActionResult<ResponseKms>> UpdateCategory( CategoryDTO  categoryUpdateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryService.UpdateCategoryAsync(categoryUpdateDTO);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        // [DEMO] Original: [Authorize(Roles = "DeptAdmin")]
        [Authorize]
        public async Task<ActionResult<ResponseKms>> CreateCategory( CategoryDTO categoryCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.CreateCategoryAsync(categoryCreationDTO);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

    }
}
