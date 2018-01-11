using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Entity.User
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CertificationAttribute : Attribute
    {
        public CertificationAttribute(string description, string ridioGroup = null)
        {
            this.Description = description;
            this.RidioGroup = ridioGroup;
        }
        public string RidioGroup { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
    }
}
