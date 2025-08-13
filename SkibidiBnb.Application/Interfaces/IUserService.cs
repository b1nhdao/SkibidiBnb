using SkibidiBnb.Application.DTO.User;

namespace SkibidiBnb.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseDTO> CreateUser(CreateUserRequestDTO createUserDto);
    }
}
