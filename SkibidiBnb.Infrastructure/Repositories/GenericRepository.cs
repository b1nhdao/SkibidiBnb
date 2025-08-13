using Microsoft.EntityFrameworkCore;
using SkibidiBnb.Domain.IRepositories;
using SkibidiBnb.Infrastructure.Data;

namespace SkibidiBnb.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly DbSet<TEntity> _dbSet;
        public readonly SkibidiBnbDbContext _context;

        public GenericRepository(SkibidiBnbDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
            _context = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if(entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetPagedAsync(int index, int size)
        {
            return await _dbSet.AsNoTracking()
                .Skip(index * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
