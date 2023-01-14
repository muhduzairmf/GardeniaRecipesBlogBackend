using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GardeniaRecipesBlogBackend.Models
{
    public class UserModel
    {
        public int Id { get; set; } = 0;

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Role { get; set; } = string.Empty;
    }
}
