using Microsoft.AspNetCore.Http;

namespace SkibidiBnb.Application.Features.Property.DTOs
{
    public class CreatePropertyDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PropertyType { get; set; }
        public int Status { get; set; }

        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public int MaxGuests { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal PricePerNight { get; set; }
        public IFormFileCollection Images { get; set; } = default!;
        public ICollection<string> ImageUrls { get; set; } = [];
        public ICollection<string> Amenities { get; set; } = [];
    }
}
