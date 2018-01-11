using Coin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User
{
    public class NoEventData
    {
        public string NickName { get; set; }
        public decimal Points { get; set; }
        public LangType LangType { get; set; }
        public string LastLoginIp { get; set; }
        public string LastLoginArea { get; set; }
    }
}
