using Test.Entity.Data;

namespace Test.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User?> GetOneByEmailAsync(string email);

    }
}