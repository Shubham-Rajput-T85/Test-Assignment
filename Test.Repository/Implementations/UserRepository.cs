using System.Threading.Tasks;
// using Test.Entity.ViewModel;
using Test.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Test.Entity.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Entity.Data;
using System.Net.Http;

namespace Test.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        
        public async Task<User?> GetOneByEmailAsync(string email)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }

}
