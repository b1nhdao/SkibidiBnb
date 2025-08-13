using SkibidiBnb.Application.Interfaces.IRepositories;
using SkibidiBnb.Domain.Entities;
using SkibidiBnb.Infrastructure.Data;

namespace SkibidiBnb.Infrastructure.Repositories
{
    public class PropertyImageRepository(SkibidiBnbDbContext dbContext) : GenericRepository<PropertyImage>(dbContext), IPropertyImageRepository
    {
        private readonly SkibidiBnbDbContext _context = dbContext;
    }
}
