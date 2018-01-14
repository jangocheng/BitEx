using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.IGrain.Entity.User.DTO
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public  class UserWithdrawalLimit
    {
        public decimal WithdrawalDayLimit { get; set; }
        public decimal WithdrawalMonthLimit { get; set; }
    }
}
