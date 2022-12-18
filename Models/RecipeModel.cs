using System.ComponentModel.DataAnnotations;

namespace GardeniaRecipesBlogBackend.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsVerified { get; set; }

        [Required]
        public double EstimatedBudget { get; set; }

        public string Contributor { get; set; } = string.Empty;

        public UserModel User { get; set; }

        public int UserId { get; set; }

        public CategoryModel Category { get; set; }

        public int CategoryId { get; set; }

        public ImageModel Images { get; set; }  
    }
}
