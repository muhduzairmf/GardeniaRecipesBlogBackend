using GardeniaRecipesBlogBackend.Data;
using GardeniaRecipesBlogBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace GardeniaRecipesBlogBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController (DataContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserModel>> SignupUser(SignupModel newSignup)
        {
            if (newSignup == null || newSignup.FullName == null || newSignup.Email == null || newSignup.Username == null || newSignup.Password == null || newSignup.Role == null)
            {
                return BadRequest();
            }

            // Validate fullname
            if (newSignup.FullName.Length < 2)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(newSignup.FullName);
            if (user != null)
            {
                return BadRequest();
            }

            // Validate email
            if (!new EmailAddressAttribute().IsValid(newSignup.Email))
            {
                return BadRequest();
            }

            // Validate username
            if (newSignup.Username.Length < 2)
            {
                return BadRequest();
            }

            user = await _context.Users.FindAsync(newSignup.Username);
            if (user != null)
            {
                return BadRequest();
            }

            // Validate password
            if (!new Regex(@".{8,}").IsMatch(newSignup.Password)) {
                return BadRequest();
            }

            if (!new Regex(@"[0-9]+").IsMatch(newSignup.Password))
            {
                return BadRequest();
            }

            if (!new Regex(@"[A-Z]+").IsMatch(newSignup.Password))
            {
                return BadRequest();
            }

            if (!new Regex(@"[a-z]+").IsMatch(newSignup.Password))
            {
                return BadRequest();
            }

            // create salt
            byte[] saltByte = new HMACSHA256().Key;

            // convert salt into string
            string salt = new System.Text.UTF8Encoding().GetString(saltByte);

            // combine salt with password
            string combinedString = salt + newSignup.Password;

            // convert that combination to byte
            byte[] combinedByte = new System.Text.UTF8Encoding().GetBytes(combinedString);

            // convert that into byte, then hash it
            byte[] hashedPassword = new HMACSHA256().ComputeHash(combinedByte);

            // then append salt with ":" and hashed (that converted into string)
            newSignup.Password = salt + ":" + new System.Text.UTF8Encoding().GetString(hashedPassword);

            UserModel newUser = new UserModel();

            newUser.FullName = newSignup.FullName;
            newUser.Email= newSignup.Email;
            newUser.Username= newSignup.Username;
            newUser.Password = newSignup.Password; 
            newUser.Role=newSignup.Role;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Create JWT Token for sign up
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, newSignup.Username),
                new Claim(ClaimTypes.Email, newSignup.Email),
                new Claim(ClaimTypes.Role, newSignup.Role),
            };

            var key = new SymmetricSecurityKey(new System.Text.UTF8Encoding().GetBytes("JWTKey"));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credential
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            newSignup.Token = jwt;

            return Ok(newSignup);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LoginModel>> LoginUser(LoginModel loginInfo)
        {
            if (loginInfo.Username == null || loginInfo.Password == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(loginInfo.Username);
            if (user == null)
            {
                return NotFound();
            }

            string salt = user.Password.Split(":")[0];
            string hashedString = user.Password.Split(":")[1];

            string combinedString = salt + loginInfo.Password;

            byte[] combinedByte = new System.Text.UTF8Encoding().GetBytes(combinedString);
            byte[] hashedPassword = new HMACSHA256().ComputeHash(combinedByte);

            if (!hashedPassword.SequenceEqual(new System.Text.UTF8Encoding().GetBytes(hashedString)))
            {
                return BadRequest();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(new System.Text.UTF8Encoding().GetBytes("JWTKey"));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credential
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            loginInfo.Token = jwt;

            return Ok(loginInfo);
        }
    }
}
