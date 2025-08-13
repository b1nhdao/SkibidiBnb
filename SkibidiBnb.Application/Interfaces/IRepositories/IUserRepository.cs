using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Application.Interfaces.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
