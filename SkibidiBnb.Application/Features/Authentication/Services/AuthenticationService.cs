using SkibidiBnb.Application.Features.Authentication.DTOs;
using SkibidiBnb.Application.Interfaces.IRepositories;
using SkibidiBnb.Application.SharedServices.Jwt;
using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Application.Features.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;

        public AuthenticationService(IUserRepository userRepository, IJwtService jwtService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }

        public async Task<LoginResponseDTO?> Login(string email, string password)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            if (user.PasswordHash != password)
            {
                return null;
            }
            
            return new LoginResponseDTO
            {
                Token = jwtService.GenerateToken(user.Id, user.Email, (int)user.Role),
                Email = user.Email,
                ExpireIn = 36000
            };
        }
    }
}
