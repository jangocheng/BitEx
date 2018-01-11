using System;
using ProtoBuf;

namespace Coin.Model.Currency
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CurrencyNameDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
