using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly DataContext _context;

        public RatingController(DataContext context)
        {
            _context = context;
        }

        // GET 		api/ratings/recipeId/${recipeId}/info
        [HttpGet("recipeId/{recipeId}/info", Name = "GetRateInfoRecipeId")]
        [ProducesResponseType(200)]
        public ActionResult<List<RatingModel>> GetRateInfoRecipeId(int recipeId)
        {
            var rating = _context.Rating.Where(s => s.RecipeId == recipeId).ToList();

            if (rating.Count == 0)
            {
                return Ok(new RatingDTO { average = 0, count = 0 });
            }

            int countRate = 0;
            int allRate = 0;
            foreach (RatingModel theRating in rating)
            {
                countRate += 1;
                allRate += theRating.star;
            }
            double averageRate = allRate / countRate;

            return Ok(new RatingDTO { average = averageRate, count = countRate });
        }

        // POST     api/ratings
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RatingModel>> CreateRating(RatingModel newRating)
        {
            _context.Rating.Add(newRating);
            await _context.SaveChangesAsync();

            return Ok(newRating);
        }

        //    [HttpGet]
        //    [ProducesResponseType(200)]
        //    public async Task<ActionResult<List<RatingModel>>> RetrieveRatings()
        //    {
        //        return Ok(await _context.Rating.ToListAsync());
        //    }

        //    [HttpGet("{id:int}", Name = "RetrieveRating")]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(400)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<RatingModel>> RetrieveRating(int id)
        //    {
        //        var rating = await _context.Rating.FindAsync(id);

        //        if (rating == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(rating);
        //    }

        //    [HttpPost]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(400)]
        //    public async Task<ActionResult<RatingModel>> CreateRecipe(RatingDTO newRating)
        //    {
        //        if (newRating == null || newRating.RateNumber <= 0 || newRating.RateNumber >= 6 || newRating.RecipeId == 0)
        //        {
        //            return BadRequest();
        //        }

        //        // Validate 

        //        RatingModel theRating = new RatingModel();
        //        theRating.RateNumber = newRating.RateNumber;
        //        theRating.RecipeId = newRating.RecipeId;

        //        _context.Rating.Add(theRating);
        //        await _context.SaveChangesAsync();

        //        return Ok(newRating);
        //    }

        //    [HttpPatch("{id:int}", Name = "UpdateRating"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<RatingModel>> UpdateRating(int id, RatingDTO updatedRating)
        //    {
        //        if (updatedRating.RateNumber <= 0 || updatedRating.RateNumber >= 6)
        //        {
        //            return BadRequest();
        //        }

        //        // Validate 

        //        var rating = await _context.Rating.FindAsync(id);
        //        if (rating == null)
        //        {
        //            return NotFound();
        //        }

        //        var recipe = await _context.Recipes.FindAsync(rating.RecipeId);
        //        if (recipe == null)
        //        {
        //            return NotFound();
        //        }

        //        rating.RateNumber = updatedRating.RateNumber;

        //        await _context.SaveChangesAsync();

        //        return Ok(rating);
        //    }

        //    [HttpDelete("{id:int}", Name = "RemoveRating"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<IActionResult> RemoveRating(int id)
        //    {
        //        var rating = await _context.Rating.FindAsync(id);
        //        if (rating == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Rating.Remove(rating);
        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }
    }
}
