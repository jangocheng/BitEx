using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Events.User
{
    public class OpenApiInvokeInfo
    {
        public DateTime StartTime { get; set; }
        /// <summary>
        /// API调用次数
        /// </summary>
        public int InvokeCount { get; set; }
    }
}
