using SkibidiBnb.Domain.Enums;

namespace SkibidiBnb.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public UserRole Role { get; set; } = UserRole.Guest;
    }
}