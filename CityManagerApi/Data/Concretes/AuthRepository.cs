using CityManagerApi.Data.Abstract;
using CityManagerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CityManagerApi.Data.Concretes
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CityManagerDbContext _context;

        public AuthRepository(CityManagerDbContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
          var user=await _context.Users.FirstOrDefaultAsync(x=>x.Username==username);
            if (user == null) return null;
            if (!VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[]? passwordSalt, byte[]? passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passHash, passSalt;
            CreatePassHash(password, out passHash, out passSalt);
            user.PasswordSalt = passSalt;   
            user.PasswordHash = passHash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePassHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using( var hmac=new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
           var hasExists=await _context.Users.AnyAsync(c=>c.Username ==username);
            return hasExists;
        }
    }
}
