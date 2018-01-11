using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using System.Linq;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawAddressRemovedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string AddressId { get; set; }
        private static string _TypeCode = typeof(WithdrawAddressRemovedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawAddressRemovedEvent()
        {
        }
        public WithdrawAddressRemovedEvent(string commandId, string addressId)
        {
            this.CommandId = commandId;
            this.AddressId = addressId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var address = modelState.WithdrawAddressList.FirstOrDefault(f => f.Id == this.AddressId);
                if (address != null)
                {
                    modelState.WithdrawAddressList.Remove(address);
                }
            }
        }
    }
}

