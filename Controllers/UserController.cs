using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UserModel>>> RetrieveUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "RetrieveUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserModel>> RetrieveUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel newUser)
        {
            if (newUser == null || newUser.FullName == null || newUser.Email == null || newUser.Username == null || newUser.Password == null || newUser.Role == null) 
            {
                return BadRequest();
            }

            // Validate fullname

            // Validate email

            // Validate username

            // Validate password

            // Hash and salt the password

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPatch("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserModel>> UpdateUser(int id, UserModel updatedUser)
        {
            if (updatedUser.FullName == null || updatedUser.Email == null || updatedUser.Username == null || updatedUser.Password == null)
            {
                return BadRequest();
            }

            // Validate fullname

            // Validate email

            // Validate username

            // Validate password

            // Hash and salt the password

            var user = await _context.Users.FindAsync(id);
            if (user == null) 
            { 
                return NotFound();
            }

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id:int}", Name = "RemoveUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
