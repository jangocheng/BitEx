using System;

namespace BitEx.IGrain.Entity
{
    public class DepositCodeDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string AccountNumber { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UsedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
