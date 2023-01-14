using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly DataContext _context;

        public RecipeController(DataContext context)
        {
            _context = context;
        }

        // POST     api/recipes
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeModel>> CreateRecipe(RecipeModel newRecipe)
        {
            _context.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();

            return Ok(newRecipe);
        }

        // GET 		api/recipes/userId/${userId}
        [HttpGet("userId/{userId}", Name = "GetRecipeFromUserId")]
        [ProducesResponseType(200)]
        public ActionResult<List<RecipeModel>> GetRecipeFromUserId(int userId)
        {
            var recipes = _context.Recipes.Where(s => s.UserId == userId).ToList();

            return Ok(recipes);
        }

        // GET 		api/recipes/id/${id}
        [HttpGet("id/{id}", Name = "GetRecipeFromId")]
        [ProducesResponseType(200)]
        public ActionResult<List<RecipeModel>> GetRecipeFromId(int id)
        {
            var recipe = _context.Recipes.Where(s => s.Id == id).ToList();

            return Ok(recipe);
        }

        // PATCH 	api/recipes/id/${id}
        [HttpPatch("id/{id}", Name = "UpdateRecipe")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<RecipeModel>>> UpdateRecipe(int id, RecipeModel theRecipe)
        {
            var recipe = _context.Recipes.Where(s => s.Id == id).ToList();

            recipe[0].RecipeName = theRecipe.RecipeName;
            recipe[0].GardeniaProduct= theRecipe.GardeniaProduct;
            recipe[0].Description= theRecipe.Description;
            recipe[0].Ingredients= theRecipe.Ingredients;
            recipe[0].CookInstruct = theRecipe.CookInstruct;
            recipe[0].Category= theRecipe.Category;
            recipe[0].EstimatedBudget= theRecipe.EstimatedBudget;
            recipe[0].IsVerified= theRecipe.IsVerified;

            await _context.SaveChangesAsync();

            return Ok(recipe);
        }

        // DELETE 	api/recipes/id/${id}
        [HttpDelete("id/{id}", Name = "DeleteRecipe")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<RecipeModel>>> DeleteRecipe(int id)
        {
            var recipe = _context.Recipes.Where(s => s.Id == id).ToList();

            _context.Recipes.Remove(recipe[0]);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET		api/recipes/id/${id}/user
        [HttpGet("id/{id}/user", Name = "GetUserFromRecipeId")]
        [ProducesResponseType(200)]
        public ActionResult<List<RecipeModel>> GetUserFromRecipeId(int id)
        {
            var recipe = _context.Recipes.Where(s => s.Id == id).ToList();

            var user = _context.Users.Where(s => s.Id == recipe[0].UserId).ToList();

            return Ok(user);
        }

        // GET		api/recipes/category/${category}
        [HttpGet("category/{category}", Name = "GetRecipeFromCategory")]
        [ProducesResponseType(200)]
        public ActionResult<List<RecipeModel>> GetRecipeFromCategory(string category)
        {
            var recipes = _context.Recipes.Where(s => s.Category == category).ToList();

            return Ok(recipes);
        }

        // GET		api/recipes/isVerified/${isVerified}
        [HttpGet("isVerified/{isVerified}", Name = "GetRecipeFromIsVerified")]
        [ProducesResponseType(200)]
        public ActionResult<List<RecipeModel>> GetRecipeFromIsVerified(bool isVerified)
        {
            var recipes = _context.Recipes.Where(s => s.IsVerified == isVerified).ToList();

            return Ok(recipes);
        }

        //    [HttpGet]
        //    [ProducesResponseType(200)]
        //    public async Task<ActionResult<List<RecipeModel>>> RetrieveRecipes()
        //    {
        //        return Ok(await _context.Recipes.ToListAsync());
        //    }

        //    [HttpGet("{id:int}", Name = "RetrieveRecipe")]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(400)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<RecipeModel>> RetrieveRecipe(int id)
        //    {
        //        var recipe = await _context.Recipes.FindAsync(id);

        //        if (recipe == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(recipe);
        //    }

        //    [HttpPost, Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(400)]
        //    public async Task<ActionResult<RecipeModel>> CreateRecipe(RecipeDTO newRecipe)
        //    {
        //        if (newRecipe == null || newRecipe.Title == null || newRecipe.Description == null || newRecipe.IsVerified == null || newRecipe.EstimatedBudget == null || newRecipe.UserId == null || newRecipe.CategoryId == null)
        //        {
        //            return BadRequest();
        //        }

        //        RecipeModel theRecipe = new RecipeModel();
        //        theRecipe.Title = newRecipe.Title;
        //        theRecipe.Description = newRecipe.Description;
        //        theRecipe.IsVerified = newRecipe.IsVerified;
        //        theRecipe.EstimatedBudget = newRecipe.EstimatedBudget;
        //        theRecipe.Contributor= newRecipe.Contributor;
        //        theRecipe.UserId = newRecipe.UserId;
        //        theRecipe.CategoryId= newRecipe.CategoryId;

        //        // Validate 

        //        _context.Recipes.Add(theRecipe);
        //        await _context.SaveChangesAsync();

        //        return Ok(theRecipe);
        //    }

        //    [HttpPatch("{id:int}", Name = "UpdateRecipe"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<RecipeModel>> UpdateRecipe(int id, RecipeDTO updatedRecipe)
        //    {
        //        if (updatedRecipe.Title == null || updatedRecipe.Description == null || updatedRecipe.EstimatedBudget == 0 || updatedRecipe.UserId == 0 || updatedRecipe.CategoryId == 0)
        //        {
        //            return BadRequest();
        //        }

        //        // Validate 

        //        var recipe = await _context.Recipes.FindAsync(id);
        //        if (recipe == null)
        //        {
        //            return NotFound();
        //        }

        //        var user = await _context.Users.FindAsync(updatedRecipe.UserId);

        //        recipe.Title = updatedRecipe.Title;
        //        recipe.Description = updatedRecipe.Description;
        //        recipe.IsVerified = false;
        //        recipe.EstimatedBudget = updatedRecipe.EstimatedBudget;
        //        recipe.Contributor = user.FullName;
        //        recipe.UserId = updatedRecipe.UserId;
        //        recipe.CategoryId = updatedRecipe.CategoryId;

        //        await _context.SaveChangesAsync();

        //        return Ok(recipe);
        //    }

        //    [HttpPatch("isVerify/{id:int}", Name = "UpdateRecipeIsVerified"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<RecipeModel>> UpdateRecipeIsVerified(int id, bool isVerify)
        //    {
        //        var recipe = await _context.Recipes.FindAsync(id);
        //        if (recipe == null)
        //        {
        //            return NotFound();
        //        }

        //        recipe.IsVerified = isVerify;

        //        await _context.SaveChangesAsync();

        //        return Ok(recipe);
        //    }

        //    [HttpDelete("{id:int}", Name = "RemoveRecipe"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<IActionResult> RemoveRecipe(int id)
        //    {
        //        var recipe = await _context.Recipes.FindAsync(id);
        //        if (recipe == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Recipes.Remove(recipe);
        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }
    }
}
