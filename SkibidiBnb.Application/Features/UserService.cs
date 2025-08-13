using SkibidiBnb.Application.DTO.User;
using SkibidiBnb.Application.Interfaces;
using SkibidiBnb.Domain.IRepositories;
using SkibidiBnb.Domain.Entities;

namespace SkibidiBnb.Application.Features
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDTO> CreateUser(CreateUserRequestDTO createUserDto)
        {
            if (createUserDto == null)
            {
                throw new ArgumentNullException(nameof(createUserDto), "Create user request cannot be null.");
            }
            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                PasswordHash = createUserDto.Password,
                PhoneNumber = createUserDto.PhoneNumber,
                DateOfBirth = createUserDto.DateOfBirth,
            };
            await _userRepository.AddAsync(user);
            return new UserResponseDTO
            {
                Fụllname = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = user.Role.ToString(),
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                DateOfBirth = user.DateOfBirth.ToLocalTime(),
                CreatedAt = user.CreatedAt.ToLocalTime(),
            };
        }
    }
}
