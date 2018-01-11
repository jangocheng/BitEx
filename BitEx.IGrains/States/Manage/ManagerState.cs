using System;
using Coin.Core.EventSourcing;
using ProtoBuf;
using System.Collections.Generic;

namespace BitEx.IGrain.States.Manage
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ManagerState : IState<int>
    {
        public int StateId { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// MD5加密密钥
        /// </summary>
        public string Salt { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public List<int> Roles { get; set; } = new List<int>();
        public bool IsPhoneRegistered { get; set; }
        public string OtpSecretKey { get; set; }
        public bool IsBindOtp { get; set; }
        public bool IsLocked { get; set; }
        public int LockedBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LockedTime { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime LastLoginTime { get; set; }
        public decimal DepositAmount { get; set; }
        public uint Version
        {
            get;
            set;
        }
    }
}
