using MangoApi.DataBaseContext;
using MangoApi.Interfaces;
using MangoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MangoApi.Services
{
    public class UserService : IUserService
    {
        private readonly MangoDbContext _context;
        public UserService(MangoDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            if (await _context.User.AnyAsync(u => u.Login == user.Login))
                return false;

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _context.User.FindAsync(user.Id);
            if (existingUser == null)
                return false;

            existingUser.Login = existingUser.Login;
            existingUser.Password = existingUser.Password;
            existingUser.Money = user.Money;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> AuthenticateAsync(string login, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
            return user;
        }


    }
}
