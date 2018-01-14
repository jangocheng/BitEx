using System;
using System.Security.Cryptography;
using System.Text;

namespace BitEx.Core.Jwt
{
    public class JwtManager
    {
        private static readonly SHA256Managed sha256 = new SHA256Managed();
        public static string GenerateSignature(Header head, Payload payload, string salt)
        {
            var stringBuilder = new StringBuilder();
            head.AppendJson(stringBuilder);
            stringBuilder.Append(salt).Append(".").Append(salt);
            payload.AppendJson(stringBuilder);
            byte[] clearBytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            var hashBytes = sha256.ComputeHash(clearBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifySignature(Header head, Payload payload, string salt, string signature)
        {
            return GenerateSignature(head, payload, salt) == signature;
        }
    }
}
