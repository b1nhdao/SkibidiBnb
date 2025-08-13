using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SkibidiBnb.Application.Common;
using SkibidiBnb.Application.DTO.Property;
using SkibidiBnb.Application.Interfaces;
using SkibidiBnb.Application.Services.UploadCloudServices;
using SkibidiBnb.Domain.Entities;
using SkibidiBnb.Domain.Enums;
using SkibidiBnb.Domain.IRepositories;


namespace SkibidiBnb.Application.Features
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUploadCloudService _uploadCloudService;

        public PropertyService(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IHttpContextAccessor httpContextAccessor, IUploadCloudService uploadCloudService)
        {
            _propertyRepository = propertyRepository;
            _httpContextAccessor = httpContextAccessor;
            _uploadCloudService = uploadCloudService;
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<PropertyResponseDTO> CreatePropertyAsync(CreatePropertyDTO createPropertyDto)
        {
            var hostId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());

            if (hostId == Guid.Empty)
            {
                return null;
            }

            var savedProperty = new Property
            {
                Id = Guid.NewGuid(),
                Title = createPropertyDto.Title,
                Description = createPropertyDto.Description,
                PropertyType = (PropertyType)createPropertyDto.PropertyType,
                Status = (PropertyStatus)createPropertyDto.Status,
                Country = createPropertyDto.Country,
                State = createPropertyDto.State,
                City = createPropertyDto.City,
                Address = createPropertyDto.Address,
                MaxGuests = createPropertyDto.MaxGuests,
                Bedrooms = createPropertyDto.Bedrooms,
                Bathrooms = createPropertyDto.Bathrooms,
                PricePerNight = createPropertyDto.PricePerNight,
                HostId = hostId,
            };

            var result = await _propertyRepository.AddAsync(savedProperty);

            ICollection<PropertyImage> propertyImages = new List<PropertyImage>();
            if (createPropertyDto.Images != null)
            {
                var imageUrls = await _uploadCloudService.UploadFilesAsync(createPropertyDto.Images);
                foreach ( var imageUrl in imageUrls )
                {
                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        continue;
                    }
                    var propertyImage = new PropertyImage
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = imageUrl,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        PropertyId = savedProperty.Id
                    };
                    var something = await _propertyImageRepository.AddAsync(propertyImage);
                    propertyImages.Add(propertyImage);
                }
            }

            var propertResult = new PropertyResponseDTO
            {
                Id = savedProperty.Id,
                Title = savedProperty.Title,
                Description = savedProperty.Description,
                PropertyType = (int)savedProperty.PropertyType,
                Status = (int)savedProperty.Status,
                FullAddress = $"{savedProperty.Address}, {savedProperty.City}, {savedProperty.State}, {savedProperty.Country}",
                MaxGuests = savedProperty.MaxGuests,
                Bedrooms = savedProperty.Bedrooms,
                Bathrooms = savedProperty.Bathrooms,
                PricePerNight = savedProperty.PricePerNight,
                HostId = savedProperty.HostId,
                ThumbnailImageUrl = propertyImages.FirstOrDefault(i => i.IsPrimary)?.ImageUrl ?? string.Empty,
                ImageUrls = propertyImages.Select(i => i.ImageUrl).ToList(),
                Amenities = createPropertyDto.Amenities
            };

            return propertResult;
        }

        public async Task<ApiResponse<PropertyResponseDTO>> GetPropertiesPagedAsync(RequestApi request)
        {
            var properties = await _propertyRepository.GetPagedAsync(request.PageIndex, request.PageSize);

            var propertyDtos = new List<PropertyResponseDTO>();
            foreach (var p in properties)
            {
                propertyDtos.Add(new PropertyResponseDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    PropertyType = (int)p.PropertyType,
                    Status = (int)p.Status,
                    FullAddress = $"{p.Address}, {p.City}, {p.State}, {p.Country}",
                    MaxGuests = p.MaxGuests,
                    Bedrooms = p.Bedrooms,
                    Bathrooms = p.Bathrooms,
                    PricePerNight = p.PricePerNight,
                    HostId = p.HostId,
                    ThumbnailImageUrl = p.Images?.FirstOrDefault(i => i.IsPrimary)?.ImageUrl ?? p.Images?.OrderBy(i => i.CreatedAt).FirstOrDefault()?.ImageUrl ?? string.Empty,
                    ImageUrls = p.Images?.Where(i => i.PropertyId == p.Id).Select(i => i.ImageUrl).ToList() ?? new List<string>(),
                    Amenities = p.Amenities?.Select(a => a.Name).ToList() ?? new List<string>()
                });
            }

            var response = new ApiResponse<PropertyResponseDTO>(
                request.PageIndex,
                request.PageSize,
                propertyDtos,
                propertyDtos.Count
            );

            return response;
        }
    }
}
