using Coin.Core.Message;
using ProtoBuf;

namespace Coin.Messages
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawAddressValidationRequest
    {
        public string AccountId { get; set; }
        public string Tag { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public bool IsVerified { get; set; }
        public string TypeCode { get; set; } = "Coin.Messages.CoinWithdrawAddressValidationResponse";
        public string Exchange { get; set; } = "Coin.Grain";
        public string RoutingKey { get; set; } = "CoinWithdraw";
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinWithdrawAddressValidationResponse : IMessage
    {
        public string TypeCode { get; set; }
        public bool Result { get; set; }
        public string AccountId { get; set; }
        public string Tag { get; set; }
        public string Address { get; set; }
        public bool IsVerified { get; set; }
        public string Memo { get; set; }
        public bool IsValid { get; set; }
    }
}
