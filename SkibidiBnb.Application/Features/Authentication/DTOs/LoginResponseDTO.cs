using SkibidiBnb.Application.Common;

namespace SkibidiBnb.Application.Features.Authentication.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ExpireIn { get; set; }
    }
}
