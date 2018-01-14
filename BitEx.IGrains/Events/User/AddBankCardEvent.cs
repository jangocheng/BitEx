using System;
using ProtoBuf;
using Orleans.Concurrency;
using Ray.Core.EventSourcing;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AddBankCardEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(AddBankCardEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Country { get; set; }
        public string Bank { get; set; }
        public string CardNumber { get; set; }
        public string NoteInfo { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public AddBankCardEvent(string country, string bank,string cardNumber,string noteInfo)
        {
            this.Country = country;
            this.Bank = bank;
            this.CardNumber = cardNumber;
            this.NoteInfo = noteInfo;
        }
        public AddBankCardEvent() { }
    }
}
