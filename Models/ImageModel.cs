using System.ComponentModel.DataAnnotations;

namespace GardeniaRecipesBlogBackend.Models
{
    public class ImageModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Path { get; set; } = string.Empty;

        public RecipeModel Recipe { get; set; } 

        public int RecipeId { get; set; }
    }
}
