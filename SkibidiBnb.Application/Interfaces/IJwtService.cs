namespace SkibidiBnb.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid id, string username, int role);
    }
}
