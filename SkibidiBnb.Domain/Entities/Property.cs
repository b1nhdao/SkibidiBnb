using System.Text.Json.Serialization;
using SkibidiBnb.Domain.Enums;

namespace SkibidiBnb.Domain.Entities
{
    public class Property : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PropertyType PropertyType { get; set; } = PropertyType.Apartment;
        public PropertyStatus Status { get; set; } = PropertyStatus.Draft;

        // Location details
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Property details
        public int MaxGuests { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal PricePerNight { get; set; }

        // Host information
        public Guid HostId { get; set; }
        public User Host { get; set; } = default!;

        [JsonIgnore]
        public ICollection<PropertyImage> Images { get; set; } = [];
        [JsonIgnore]
        public ICollection<Amenity> Amenities { get; set; } = [];
    }
}
