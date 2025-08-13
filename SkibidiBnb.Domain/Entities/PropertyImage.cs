using System.Text.Json.Serialization;

namespace SkibidiBnb.Domain.Entities
{
    public class PropertyImage : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public bool IsPrimary { get; set; } = false;
        public Guid PropertyId { get; set; }
        [JsonIgnore]
        public Property Property { get; set; } = default!;
    }
}
