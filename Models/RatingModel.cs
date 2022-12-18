namespace GardeniaRecipesBlogBackend.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        public int rateNumber { get; set; }

        public RecipeModel Recipes { get; set; }
    }
}
