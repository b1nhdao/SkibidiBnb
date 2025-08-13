using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Domain.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUserByEmailAsync(string email);
    }
}
