using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UserModel>>> RetrieveUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "RetrieveUser"), Authorize]
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

        [HttpPatch("{id:int}", Name = "UpdateUser"), Authorize]
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

        [HttpDelete("{id:int}", Name = "RemoveUser"), Authorize]
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
