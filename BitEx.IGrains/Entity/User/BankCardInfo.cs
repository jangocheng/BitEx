using Coin.Framework.ThirdParty;
using ProtoBuf;
using BitEx.IGrain.States;
using Orleans.Concurrency;

namespace BitEx.IGrain.Entity.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class BankCardInfo
    {
        public string Id { get; set; }
        public BankType BankType { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Bank { get; set; }
        public string BranchBank { get; set; }
        public string CardNumber { get; set; }
    }
}
