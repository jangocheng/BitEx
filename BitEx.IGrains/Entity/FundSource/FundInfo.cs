using BitEx.IGrain.States;

namespace BitEx.IGrain.Entity
{
    public class FundInfo
    {
        public string CapitalAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public DepositStatus Status { get; set; }
        public DepositWay DepositWay { get; set; }
    }
}
