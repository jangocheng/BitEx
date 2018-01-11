using System;
using System.Linq;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class DeleteBankCardEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(DeleteBankCardEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string CardId { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public DeleteBankCardEvent(string id)
        {
            this.CardId = id;
        }
        public DeleteBankCardEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var card = modelState.BankCardList.FirstOrDefault(b => b.Id.Equals(this.CardId));
                if (card != null)
                {
                    modelState.BankCardList.Remove(card);
                }
            }
        }
    }
}
