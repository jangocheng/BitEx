using System;
using System.Text;

namespace BitEx.Core.Jwt
{
    public class Header
    {
        public string Type { get; set; }
        public string Alg { get; set; }
        public void AppendJson(StringBuilder builder)
        {
            builder.Append("{'Type':'").Append(Type).Append("','Alg':'").Append(Alg).Append("'}");
        }
        string base64Json;
        public string ToBase64Json()
        {
            if (string.IsNullOrEmpty(base64Json))
            {
                base64Json = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{{'Type':'{Type}','Alg':'{Alg}'}}"));
            }
            return base64Json;
        }
    }
}
