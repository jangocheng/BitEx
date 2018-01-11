using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CoinWithdrawHandlingFailedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public CoinWithdrawStatus Status { get; set; }
        private static string _TypeCode = typeof(CoinWithdrawHandlingFailedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CoinWithdrawHandlingFailedEvent()
        {
        }
        public CoinWithdrawHandlingFailedEvent(string commandId, string message)
        {
            this.CommandId = commandId;
            this.Message = message;
            this.Status = CoinWithdrawStatus.ProcessingFailed;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinWithdrawState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
            }
        }
    }
}
