using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class WithdrawAddressAddedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string AddressId { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public string Tag { get; set; }
        public bool IsVerified { get; set; }
        public bool IsValid { get; set; }
        private static string _TypeCode = typeof(WithdrawAddressAddedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public WithdrawAddressAddedEvent()
        {
        }
        public WithdrawAddressAddedEvent(string commandId, string addressId, string userId, string currencyId, string address, string memo, string tag, bool isVerified,bool isValid)
        {
            this.CommandId = commandId;
            this.AddressId = addressId;
            this.UserId = userId;
            this.CurrencyId = currencyId;
            this.Address = address;
            this.Tag = tag;
            this.IsVerified = isVerified;
            this.Memo = memo;
            this.IsValid = isValid;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CoinAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.WithdrawAddressList.Add(new WithdrawAddress
                {
                    Id = this.AddressId,
                    Address = this.Address,
                    Memo = this.Memo,
                    Tag = this.Tag,
                    IsVerified = this.IsVerified,
                    IsValid = this.IsValid
                });
            }
        }
    }
}

