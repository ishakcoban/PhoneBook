using System.Security.Cryptography;

namespace PhoneBook.Services.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes = 256 bits
                byte[] saltedHash = new byte[48]; // 16 bytes for salt + 32 bytes for hash
                Buffer.BlockCopy(salt, 0, saltedHash, 0, 16);
                Buffer.BlockCopy(hash, 0, saltedHash, 16, 32);
                return Convert.ToBase64String(saltedHash);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] saltedHash = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Buffer.BlockCopy(saltedHash, 0, salt, 0, 16);

            string newHashedPassword = HashPassword(password, salt);
            return hashedPassword == newHashedPassword;
        }
    }

    public class SaltGenerator
    {
        public static byte[] GenerateSalt(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Salt length must be greater than zero.");
            }

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[length];
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
