using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebUniDiaryTwo.Services
{
    public class UserService
    {
        private readonly UniversityContext _context;

        public UserService(UniversityContext context)
        {
            _context = context;
        }

        public User GetUserWithRoles(int userId)
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Id == userId);

            return user;
        }

        // TODO Implement
        public string EncryptPassword(string password)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<string>();
            try
            {
                var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
                return result == PasswordVerificationResult.Success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
