using Microsoft.EntityFrameworkCore;
using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Infrastructure.Data
{
    public class SkibidiBnbDbContext : DbContext
    {
        public SkibidiBnbDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SkibidiBnbDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
    }
}
