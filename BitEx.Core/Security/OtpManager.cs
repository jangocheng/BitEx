using System;
using OtpNet;

namespace BitEx.Core.Security
{
    public class OtpManager
    {
        public static string CreateSecretKey()
        {
            var bytes = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(bytes);
        }

        public static bool VerifyOtp(string secretKey, string code)
        {
            if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(code))
            {
                return false;
            }
            var base32Bytes = Base32Encoding.ToBytes(secretKey);

            var otp = new Totp(base32Bytes);
            return otp.ComputeTotp(DateTime.UtcNow) == code;
        }
    }
}
