using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class BreedDbContext : DbContext
    {
        public BreedDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }



        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Traits> Traits { get; set; }
    }
}
