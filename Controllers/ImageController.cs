using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly DataContext _context;

        public ImageController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<ImageModel>>> RetrieveImages()
        {
            return Ok(await _context.Images.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "RetrieveImage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ImageModel>> RetrieveImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ImageModel>> CreateImage(ImageModel newImage)
        {
            if (newImage == null || newImage.Path == null || newImage.RecipeId == null)
            {
                return BadRequest();
            }

            _context.Images.Add(newImage);
            await _context.SaveChangesAsync();

            return Ok(newImage);
        }

        [HttpPatch("{id:int}", Name = "UpdateImage"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ImageModel>> UpdateImage(int id, ImageModel updatedImage)
        {
            if (updatedImage.Path == null)
            {
                return BadRequest();
            }

            // Validate path

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            image.Path = updatedImage.Path;

            await _context.SaveChangesAsync();

            return Ok(image);
        }

        [HttpDelete("{id:int}", Name = "RemoveImage"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
