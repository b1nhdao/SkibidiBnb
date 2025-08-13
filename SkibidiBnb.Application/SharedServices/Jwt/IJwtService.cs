namespace SkibidiBnb.Application.SharedServices.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(Guid id, string username, int role);
    }
}
