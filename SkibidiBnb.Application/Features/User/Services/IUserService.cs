using SkibidiBnb.Application.Common;
using SkibidiBnb.Application.Features.User.DTOs;

namespace SkibidiBnb.Application.Features.User.Services
{
    public interface IUserService
    {
        Task<Result<UserResponseDTO>> CreateUser(CreateUserRequestDTO createUserDto);
        Guid GetCurrentUserId();
    }
}
