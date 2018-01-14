using BitEx.Core;
using BitEx.Model.User;
using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.IGrain.Entity.User.DTO
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UserBaseInfo
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        /// <summary>
        /// VIP等级
        /// </summary>
        public UserVipLevel VipLevel { get; set; }
        public PromoteLevel PromoteLevel { get; set; }
    }
}
