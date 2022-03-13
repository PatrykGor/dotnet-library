using Biblioteka.Repository;
using Biblioteka.Models;
using Biblioteka.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Biblioteka.Services
{
    public class UserService : IUserService
    {
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        private readonly IRepository<User> _userRepository;
        public string Login(string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            User user = _userRepository.Get(u => u.Username == username);
            if (user == null)
                throw new ArgumentException(nameof(username));
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: user.PasswordSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            if (hashedPassword == user.PasswordHash)
                return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}{DateTime.Today}"));
            else
                throw new ArgumentException(nameof(password));
        }

        public void Register(string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (_userRepository.GetMultiple(u => u.Username == username).Any())
                throw new ArgumentException(nameof(username));
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: password,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8));
            _userRepository.Insert(new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                PasswordSalt = salt
            });
        }
    }
}
