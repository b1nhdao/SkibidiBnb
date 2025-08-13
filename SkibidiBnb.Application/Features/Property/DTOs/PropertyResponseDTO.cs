namespace SkibidiBnb.Application.Features.Property.DTOs
{
    public class PropertyResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PropertyType { get; set; }
        public int Status { get; set; }
        public string FullAddress { get; set; } = string.Empty;
        public int MaxGuests { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal PricePerNight { get; set; }
        public Guid HostId { get; set; }
        public string ThumbnailImageUrl { get; set; } = string.Empty;
        public ICollection<string> ImageUrls { get; set; } = new List<string>();
        public ICollection<string> Amenities { get; set; } = new List<string>();
    }
}
