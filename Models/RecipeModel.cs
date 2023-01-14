using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardeniaRecipesBlogBackend.Models
{
    public class RecipeModel
    {
        public int Id { get; set; } = 0;

        [Required]
        [MaxLength(255)]
        public string RecipeName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string GardeniaProduct { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "Text")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "Text")]
        public string Ingredients { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "Text")]
        public string CookInstruct { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string RecipeImg { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CreatedDate { get; set; } = string.Empty;

        [Required]
        public bool IsVerified { get; set; } = false;

        [Required]
        public double EstimatedBudget { get; set; } = 0;

        public int UserId { get; set; } = 0;
    }
}
