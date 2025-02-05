using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Model;
using ProductManagementAPI.Repositories;

namespace ProductManagementAPI.Controllers
{
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _repo;
        public CategoryController(CategoryRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetCategories() => Ok(await _repo.GetCategories());

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.Name)) return BadRequest("Category name is required.");
            await _repo.AddCategory(category.Name);
            return Ok("Category added.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            await _repo.UpdateCategory(id, category.Name);
            return Ok("Category updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _repo.DeleteCategory(id);
            return Ok("Category deleted.");
        }
    }

}
