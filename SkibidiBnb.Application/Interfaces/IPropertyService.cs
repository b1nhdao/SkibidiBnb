using SkibidiBnb.Application.Common;
using SkibidiBnb.Application.DTO.Property;

namespace SkibidiBnb.Application.Interfaces
{
    public interface IPropertyService
    {
        public Task<PropertyResponseDTO> CreatePropertyAsync(CreatePropertyDTO createPropertyDto);
        public Task<ApiResponse<PropertyResponseDTO>> GetPropertiesPagedAsync(RequestApi request);
    }
}
