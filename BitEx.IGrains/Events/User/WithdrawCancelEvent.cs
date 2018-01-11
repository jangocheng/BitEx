using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class WithdrawCancelEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(WithdrawCancelEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public decimal Amount { get; set; }
        public DateTime WithdrawTime { get; set; }
        public DateTime Timestamp { get; set; }
        public UInt32 Version { get; set; }
        public WithdrawCancelEvent(decimal amount, DateTime withdrawTime)
        {
            Amount = amount;
            this.WithdrawTime = withdrawTime;
        }
        public WithdrawCancelEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                if ((Timestamp - WithdrawTime).Days == 0)
                    modelState.WithdrawalDayLimit -= Amount;
                if (Timestamp.Month == WithdrawTime.Month)
                    modelState.WithdrawalMonthLimit -= Amount;
            }
        }
    }
}
