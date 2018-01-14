using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BitEx.Core.Security
{
    public class PasswordManager
    {
        private static readonly SHA256Managed sha256 = new SHA256Managed();
        public static string Encrypt(string password, string salt)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashBytes = sha256.ComputeHash(clearBytes);
            var builder = new StringBuilder();
            for (int x = 0; x < hashBytes.Length; x++)
            {
                builder.Append(hashBytes[x].ToString("x"));
            }
            return builder.ToString();
        }
        public static bool VerifyPassword(string password, string salt, string hashPassword)
        {
            return Encrypt(password, salt) == hashPassword;
        }
    }
}
