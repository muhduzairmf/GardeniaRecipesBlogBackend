namespace GardeniaRecipesBlogBackend.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        public int RateNumber { get; set; }

        public RecipeModel Recipes { get; set; }

        public int RecipeId { get; set; }
    }
}
