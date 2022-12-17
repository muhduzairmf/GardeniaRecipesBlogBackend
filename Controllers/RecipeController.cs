using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly DataContext _context;

        public RecipeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<RecipeModel>>> RetrieveRecipes()
        {
            return Ok(await _context.Recipes.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "RetrieveRecipe")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RecipeModel>> RetrieveRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RecipeModel>> CreateRecipe(RecipeModel newRecipe)
        {
            if (newRecipe == null || newRecipe.Title == null || newRecipe.Description == null || newRecipe.IsVerified == null || newRecipe.EstimatedBudget == null || newRecipe.UserId == null || newRecipe.CategoryId == null)
            {
                return BadRequest();
            }

            // Validate 

            _context.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();

            return Ok(newRecipe);
        }

        [HttpPatch("{id:int}", Name = "UpdateRecipe")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RecipeModel>> UpdateRecipe(int id, RecipeModel updatedRecipe)
        {
            if (updatedRecipe.Title == null || updatedRecipe.Description == null || updatedRecipe.IsVerified == null || updatedRecipe.EstimatedBudget == null || updatedRecipe.UserId == null || updatedRecipe.CategoryId == null)
            {
                return BadRequest();
            }

            // Validate 

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Title = updatedRecipe.Title;
            recipe.Description = updatedRecipe.Description;
            recipe.IsVerified = updatedRecipe.IsVerified;
            recipe.EstimatedBudget = updatedRecipe.EstimatedBudget;
            recipe.UserId = updatedRecipe.UserId;
            recipe.CategoryId = updatedRecipe.CategoryId;

            await _context.SaveChangesAsync();

            return Ok(recipe);
        }

        [HttpDelete("{id:int}", Name = "RemoveRecipe")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
