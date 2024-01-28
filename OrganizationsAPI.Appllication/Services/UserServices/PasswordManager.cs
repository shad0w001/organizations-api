using OrganizationsAPI.Appllication.Interfaces.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Services.UserServices
{
    public class PasswordManager : IPasswordManager
    {
        private const int _keySize = 64;
        private const int _iterations = 500;
        private HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;


        public string HashPassword(string password, out string salt)
        {
            byte[] saltByteArray = RandomNumberGenerator.GetBytes(_keySize);
            salt = Convert.ToHexString(saltByteArray);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltByteArray,
                _iterations,
                _hashAlgorithm,
                _keySize);

            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            byte[] hashFromPass = Rfc2898DeriveBytes.Pbkdf2(
                password,
                Convert.FromHexString(salt),
                _iterations,
                _hashAlgorithm,
                _keySize);

            return CryptographicOperations.FixedTimeEquals(hashFromPass, Convert.FromHexString(hashedPassword));
        }
    }
}
