using System;

namespace BitEx.Model.User.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public short VipLevel { get; set; }
        public string RealName { get; set; }
        public bool IsPhoneRegistered { get; set; }
        public short VerifyLevel { get; set; }
        public bool IsBindOtp { get; set; }
        public string OtpSecretKey
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IsBindOtp = true;
            }
        }
        public short LockType { get; set; }
        public DateTime CreateTime { get; set; }
        public short Status { get; set; }
    }
}
