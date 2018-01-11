using BitEx.IGrain.States;

namespace BitEx.IGrain.Entity
{
    public class GuaranteeInfo
    {
        public string DepositId { get; set; }
        public string GuaranteeId { get; set; }
        public bool IsVirtualCoin { get; set; }
        public string AccountId { get; set; }
        public string MortgagedAccountId { get; set; }
        public decimal MortgagedAmount { get; set; }
        public decimal DepositAmount { get; set; }        
        public GuaranteeStatus Status { get; set; }
    }
}
