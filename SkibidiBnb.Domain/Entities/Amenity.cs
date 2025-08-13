using SkibidiBnb.Domain.Enums;

namespace SkibidiBnb.Domain.Entities
{
    public class Amenity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public AmenityCategory Category { get; set; }
    }
}
