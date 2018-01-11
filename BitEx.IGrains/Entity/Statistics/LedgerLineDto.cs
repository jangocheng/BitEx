using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class LedgerLineDto
    {
        public string Title { get; set; }
        public Dictionary<string, string> LineNames = new Dictionary<string, string>() { { "PlatBalance", "系统金额" }, { "BookBalance", "账本金额" } };
        public IEnumerable<LedgerLine> Data { get; set; }
    }
}
