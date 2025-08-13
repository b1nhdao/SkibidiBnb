using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Application.Interfaces.IRepositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<IEnumerable<Property>> GetAllPropertiesByHostIdAsync(Guid hostId);
    }
}
