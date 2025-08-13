using Microsoft.EntityFrameworkCore;
using SkibidiBnb.Domain.Entities;
using SkibidiBnb.Domain.IRepositories;
using SkibidiBnb.Infrastructure.Data;

namespace SkibidiBnb.Infrastructure.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        private readonly SkibidiBnbDbContext _context;
        public PropertyRepository(SkibidiBnbDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesByHostIdAsync(Guid hostId)
        {
            return await _context.Properties.AsNoTracking()
                .Where(p => p.HostId == hostId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Amenities)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Property?>> GetPagedAsync(int index, int size)
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Amenities)
                .AsNoTracking()
                .Skip(index * size)
                .Take(size)
                .ToListAsync();
        }
    }
}
