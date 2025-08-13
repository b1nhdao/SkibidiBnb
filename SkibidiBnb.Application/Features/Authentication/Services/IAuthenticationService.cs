using SkibidiBnb.Application.Features.Authentication.DTOs;
using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Application.Features.Authentication.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO?> Login(string email, string password);
    }
}
