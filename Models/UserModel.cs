using System.ComponentModel.DataAnnotations;

namespace GardeniaRecipesBlogBackend.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string Role { get; set; }
    }
}
