using Microsoft.EntityFrameworkCore;
using SkibidiBnb.Domain.Entities;
using SkibidiBnb.Domain.IRepositories;
using SkibidiBnb.Infrastructure.Data;

namespace SkibidiBnb.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SkibidiBnbDbContext _context;

        public UserRepository(SkibidiBnbDbContext _context) : base(_context)
        {
            this._context = _context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
             return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
