using SkibidiBnb.Domain.Entities;
using SkibidiBnb.Domain.IRepositories;
using SkibidiBnb.Infrastructure.Data;

namespace SkibidiBnb.Infrastructure.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImage>, IPropertyImageRepository
    {
        private readonly SkibidiBnbDbContext _context;
        public PropertyImageRepository(SkibidiBnbDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
