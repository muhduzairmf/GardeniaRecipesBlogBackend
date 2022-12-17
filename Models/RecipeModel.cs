using System.ComponentModel.DataAnnotations;

namespace GardeniaRecipesBlogBackend.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public bool IsVerified { get; set; }

        [Required]
        public double EstimatedBudget { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
