using BitEx.IGrain.States;

namespace BitEx.IGrain.Entity
{
    public class CoinDepositConfirmation
    {
        public string DepositId { get; set; }
        public string TxNo { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }      
        public decimal Volume { get; set; }
        public int Confirmation { get; set; }
        public CoinDepositStatus Status { get; set; }
    }
}
