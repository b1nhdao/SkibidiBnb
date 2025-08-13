using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SkibidiBnb.Application.SharedServices.Jwt
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(Guid id,string email, int role)
        {
            var issuer = "https://localhost:7066";
            var audience = "https://localhost:7066";
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("your-256-bit-secret-your-256-bit-secret"));
            var expiration = DateTime.UtcNow.AddHours(100);

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
