using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<CategoryModel>>> RetrieveCategories()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "RetrieveCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CategoryModel>> RetrieveCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CategoryModel>> CreateUser(CategoryModel newCategory)
        {
            if (newCategory == null || newCategory.Name == null)
            {
                return BadRequest();
            }

            // Validate name
            
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return Ok(newCategory);
        }

        [HttpPatch("{id:int}", Name = "UpdateCategory"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CategoryModel>> UpdateCategory(int id, CategoryModel updatedCategory)
        {
            if (updatedCategory.Name == null)
            {
                return BadRequest();
            }

            // Validate name

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;

            await _context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpDelete("{id:int}", Name = "RemoveCategory"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
