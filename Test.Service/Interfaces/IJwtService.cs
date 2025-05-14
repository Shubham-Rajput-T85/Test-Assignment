using System.Security.Claims;

namespace Test.Service.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(string name,string role,DateTime expireTime);

    ClaimsPrincipal? ValidateToken(string token);
}
