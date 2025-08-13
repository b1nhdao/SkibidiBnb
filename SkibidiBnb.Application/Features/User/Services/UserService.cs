using SkibidiBnb.Domain.Entities;
using SkibidiBnb.Application.Common;
using Microsoft.AspNetCore.Http;
using SkibidiBnb.Application.Interfaces.IRepositories;
using SkibidiBnb.Application.Features.User.DTOs;

namespace SkibidiBnb.Application.Features.User.Services
{
    public class UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<UserResponseDTO>> CreateUser(CreateUserRequestDTO createUserDto)
        {
            if (createUserDto == null)
            {
                return Result<UserResponseDTO>.Failure("CreateUserRequestDTO cannot be null.");
            }
            var user = new Domain.Entities.User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                PasswordHash = createUserDto.Password,
                PhoneNumber = createUserDto.PhoneNumber,
                DateOfBirth = createUserDto.DateOfBirth,
            };
            await _userRepository.AddAsync(user);
            return Result<UserResponseDTO>.Success(new UserResponseDTO
            {
                Fụllname = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = user.Role.ToString(),
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                DateOfBirth = user.DateOfBirth.ToLocalTime(),
                CreatedAt = user.CreatedAt.ToLocalTime(),
            });
        }

        public Guid GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Guid.Empty;
            }
            return userId;
        }
    }
}
