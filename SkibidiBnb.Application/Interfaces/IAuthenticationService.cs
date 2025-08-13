using SkibidiBnb.Application.DTO.Authentication;
using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO?> Login(string email, string password);
    }
}
