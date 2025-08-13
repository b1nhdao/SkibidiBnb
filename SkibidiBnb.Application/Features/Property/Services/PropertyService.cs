using SkibidiBnb.Application.Common;
using SkibidiBnb.Application.Features.Property.DTOs;
using SkibidiBnb.Application.Features.User.Services;
using SkibidiBnb.Application.Interfaces.IRepositories;
using SkibidiBnb.Application.SharedServices.UploadCloud;
using SkibidiBnb.Domain.Enums;


namespace SkibidiBnb.Application.Features.Property.Services
{
    public class PropertyService(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IUserService userService, IUploadCloudService uploadCloudService) : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository = propertyImageRepository;
        private readonly IUserService _userService = userService;
        private readonly IUploadCloudService _uploadCloudService = uploadCloudService;

        public async Task<Result<PropertyResponseDTO>> CreatePropertyAsync(CreatePropertyDTO createPropertyDto)
        {
            var hostId = _userService.GetCurrentUserId();

            if (hostId == Guid.Empty)
            {
                return Result<PropertyResponseDTO>.Failure("User not authenticated");
            }

            var savedProperty = new Domain.Entities.Property
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

            ICollection<Domain.Entities.PropertyImage> propertyImages = [];
            if (createPropertyDto.Images != null)
            {
                var imageUrls = await _uploadCloudService.UploadMultipleFilesAsync(createPropertyDto.Images);
                foreach ( var imageUrl in imageUrls.Value )
                {
                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        continue;
                    }
                    var propertyImage = new Domain.Entities.PropertyImage
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

            return Result<PropertyResponseDTO>.Success(propertResult);
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
                    ImageUrls = p.Images?.Where(i => i.PropertyId == p.Id).Select(i => i.ImageUrl).ToList() ?? [],
                    Amenities = p.Amenities?.Select(a => a.Name).ToList() ?? []
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
