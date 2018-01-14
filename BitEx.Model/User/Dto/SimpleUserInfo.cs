using System;

namespace BitEx.Model.User.Dto
{
    public class SimpleUserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneRegistered { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
