using System;

namespace BitEx.IGrain.Entity
{
    public class DividendDto
    {
        public int Id { get; set; }
        public string CurrencyId { get; set; }
        public string Period { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime? AllottedAt { get; set; }
        public string HeldVolume { get; set; }
        public string PayAmount { get; set; }
        public bool IsAllotted { get; set; }
    }
}
