using SkibidiBnb.Application.Common;

namespace SkibidiBnb.Application.DTO.Authentication
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ExpireIn { get; set; }
    }
}
