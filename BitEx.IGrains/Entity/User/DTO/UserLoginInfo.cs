using Coin.Core;
using Orleans.Concurrency;
using ProtoBuf;
using System;

namespace BitEx.IGrain.Entity.User.DTO
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UserLoginInfo
    {
        public string UserId { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public LangType LangType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public BindType BindType { get; set; }
        public bool IsPhoneRegistered { get; set; }
        public TradePasswordType TradePasswordType { get; set; }
        public UserVipLevel VipLevel { get; set; }
        public int VerifyLevel { get; set; }
        public UserLockType LockType { get; set; }
        public string CountryCode { get; set; }
    }
}
