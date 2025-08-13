using SkibidiBnb.Application.Common;
using SkibidiBnb.Application.Features.Property.DTOs;

namespace SkibidiBnb.Application.Features.Property.Services
{
    public interface IPropertyService
    {
        Task<Result<PropertyResponseDTO>> CreatePropertyAsync(CreatePropertyDTO createPropertyDto);
        Task<ApiResponse<PropertyResponseDTO>> GetPropertiesPagedAsync(RequestApi request);
    }
}
