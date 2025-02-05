using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Model;

namespace ProductManagementAPI.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly DapperDbContext _context;

        public ProductController(DapperDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            string filePath = Path.Combine(_environment.WebRootPath, "images", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { ImageUrl = "/images/" + file.FileName });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync("INSERT INTO Products (Name, CategoryId, ImageUrl) VALUES (@Name, @CategoryId, @ImageUrl)", product);
            return Ok("Product added.");
        }
    }

}
