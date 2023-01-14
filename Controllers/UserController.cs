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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        // GET      api/users/username/${username}
        [HttpGet("username/{username}", Name = "GetUser")]
        [ProducesResponseType(200)]
        public ActionResult<List<RatingModel>> GetUser(string username)
        {
            var user = _context.Users.Where(s => s.Username == username).ToList();

            return Ok(user);
        }

        // POST     api/users
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        // PATCH    api/users/id/${id}
        [HttpPatch("id/{id}", Name = "UpdateUser")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserModel>> UpdateUser(int id, UserModel newUser)
        {
            var user = _context.Users.Where(s => s.Id == id).ToList();

            user[0].FullName = newUser.FullName;
            user[0].Username = newUser.Username;
            user[0].Email = newUser.Email;
            user[0].Password = newUser.Password;

            await _context.SaveChangesAsync();

            return Ok(user[0]);
        }


        //    [HttpGet, Authorize]
        //    [ProducesResponseType(200)]
        //    public async Task<ActionResult<List<UserModel>>> RetrieveUsers()
        //    {
        //        return Ok(await _context.Users.ToListAsync());
        //    }

        //    [HttpGet("{id:int}", Name = "RetrieveUser"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(400)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<UserModel>> RetrieveUser(int id)
        //    {
        //        var user = await _context.Users.FindAsync(id);

        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(user);
        //    }

        //    [HttpPatch("{id:int}", Name = "UpdateUser"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<UserModel>> UpdateUser(int id, UserDTO updatedUser)
        //    {
        //        if (updatedUser.FullName == null || updatedUser.Email == null || updatedUser.Username == null)
        //        {
        //            return BadRequest();
        //        }

        //        var user = await _context.Users.FindAsync(id);
        //        if (user == null) 
        //        { 
        //            return NotFound();
        //        }

        //        user.FullName = updatedUser.FullName;
        //        user.Email = updatedUser.Email;
        //        user.Username = updatedUser.Username;

        //        await _context.SaveChangesAsync();

        //        return Ok(user);
        //    }

        //    [HttpPatch("password/{id:int}", Name = "UpdateUserPassword"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<ActionResult<UserModel>> UpdateUserPassword(int id, string currentPassword, string newPassword)
        //    {
        //        var user = await _context.Users.FindAsync(id);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        string salt = user.Password.Split(":")[0];
        //        string hashedString = user.Password.Split(":")[1];

        //        string combinedString = salt + currentPassword;

        //        byte[] combinedByte = Encoding.UTF8.GetBytes(combinedString);
        //        string hashedPassword = Convert.ToHexString(SHA256.HashData(combinedByte)).Replace("-", "");

        //        if (hashedPassword != hashedString)
        //        {
        //            return BadRequest();
        //        }

        //        // create salt
        //        // convert salt into string
        //        string newSalt = Convert.ToHexString(new HMACSHA256().Key).Replace("-", "");

        //        // combine salt with password
        //        string newCombinedString = salt + newPassword;

        //        // convert that combination to byte
        //        byte[] newCombinedByte = Encoding.UTF8.GetBytes(newCombinedString);

        //        // hash that combination, then convert to string
        //        string newHashedPassword = Convert.ToHexString(SHA256.HashData(newCombinedByte)).Replace("-", "");

        //        // then append salt with ":" and hashed (that converted into string)
        //        user.Password = newSalt + ":" + newHashedPassword;

        //        await _context.SaveChangesAsync();

        //        return Ok(user);
        //    }

        //    [HttpDelete("{id:int}", Name = "RemoveUser"), Authorize]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(404)]
        //    public async Task<IActionResult> RemoveUser(int id)
        //    {
        //        var user = await _context.Users.FindAsync(id);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Users.Remove(user);
        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }
    }
}
