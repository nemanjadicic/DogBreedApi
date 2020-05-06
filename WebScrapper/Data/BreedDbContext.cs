using Microsoft.EntityFrameworkCore;
using WebScrapper.Models;

namespace WebScrapper.Data
{
    public class BreedDbContext : DbContext
    {
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Traits> Traits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DogBreedDB;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
