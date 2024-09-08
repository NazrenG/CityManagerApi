using CityManagerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityManagerApi.Data
{
    public class CityManagerDbContext : DbContext
    {
        public CityManagerDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityImage> Images { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
