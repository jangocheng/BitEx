using System;

namespace BitEx.Model.Account
{
    public class AccountBalanceDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public decimal LockedAmount { get; set; }
        public decimal MortgagedAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AccountStatus Status { get; set; }
    }
}
