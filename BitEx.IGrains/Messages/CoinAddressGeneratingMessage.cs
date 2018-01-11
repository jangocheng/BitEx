using Coin.Core.Message;
using ProtoBuf;

namespace Coin.Messages
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAddressGeneratingRequest
    {
        public string CommandId { get; set; }
        public string UserId { get; set; }
        public string AccountId { get; set; }
        public string CurrencyId { get; set; }
        public string TypeCode { get; set; } = "Coin.Messages.CoinAddressGeneratingResponse";
        public string Exchange { get; set; } = "Coin.Grain";
        public string RoutingKey { get; set; } = "CoinAccount_0";
        public CoinAddressGeneratingRequest()
        {
        }
        public CoinAddressGeneratingRequest(string commandId, string userId, string accountId, string currencyId)
        {
            this.CommandId = commandId;
            this.UserId = userId;
            this.AccountId = accountId;
            this.CurrencyId = currencyId;
        }
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CoinAddressGeneratingResponse : IMessage
    {
        public string CommandId { get; set; }
        public string AccountId { get; set; }
        public string Address { get; set; }
        public string CurrencyId { get; set; }
        public string TypeCode { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public CoinAddressGeneratingResponse()
        {
        }
        public CoinAddressGeneratingResponse(string message)
        {
            this.Result = false;
            this.Message = message;
        }
        public CoinAddressGeneratingResponse(string commandId, string accountId, string address, string currencyId)
        {
            this.CommandId = commandId;
            this.AccountId = accountId;
            this.Address = address;
            this.CurrencyId = currencyId;
            this.Result = true;
        }
    }
}
