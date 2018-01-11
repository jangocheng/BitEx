using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin.Model.User.Dto
{
    public class SimpleUserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneRegistered { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
