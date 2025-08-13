using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SkibidiBnb.Infrastructure.Data
{
    public class SkibidiBnbDbContextFactory : IDesignTimeDbContextFactory<SkibidiBnbDbContext>
    {
        public SkibidiBnbDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SkibidiBnbDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SkibidiBnbDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            return new SkibidiBnbDbContext(optionsBuilder.Options);
        }
    }
}
