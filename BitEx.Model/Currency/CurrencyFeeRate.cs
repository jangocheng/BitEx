using ProtoBuf;

namespace Coin.Model.Currency
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CurrencyFeeRate
    {
        public string Id { get; set; }
        public decimal DepositFixedFee { get; set; }
        public decimal DepositFeeRate { get; set; }
        public decimal WithdrawFeeRate { get; set; }
        public decimal WithdrawFixedFee { get; set; }
    }
}
