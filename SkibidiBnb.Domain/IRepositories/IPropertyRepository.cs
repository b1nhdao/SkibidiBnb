using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Domain.IRepositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<IEnumerable<Property>> GetAllPropertiesByHostIdAsync(Guid hostId);
    }
}
