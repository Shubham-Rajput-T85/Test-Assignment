using Test.Entity.Data;
using Test.Repository.Interfaces;
using Test.Service.Interfaces;
using Test.Service.Utils;
namespace Test.Service.Implementation;

public class AuthService : IAuthService
{
        private readonly Context _context;
        private readonly IUserRepository _repository;
        public AuthService(Context context,IUserRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<User?> AuthenticateUser(string email, string password)
        {
            // var user = await _context.Users.Include(u => u.Role)
            //                                .FirstOrDefaultAsync(u => u.Email == email);

            var user = await _repository.GetOneByEmailAsync(email);

            // if (user == null || !PasswordUtils.VerifyPassword(password, user.Password))
            //     return null;
            if(user != null){
                var hashpassword = PasswordUtils.VerifyPassword(password, user.Password);
            }
            

            if (user == null || !(password == user.Password))
                return null;

            return user;
        }


}
