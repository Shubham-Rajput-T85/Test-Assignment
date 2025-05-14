using Test.Entity.Data;

namespace Test.Service.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateUser(string email, string password);
    
}
