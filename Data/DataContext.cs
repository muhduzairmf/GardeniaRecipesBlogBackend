using GardeniaRecipesBlogBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GardeniaRecipesBlogBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<RecipeModel> Recipes { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<RatingModel> Rating { get; set; }
    }
}
