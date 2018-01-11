using System;
using System.Collections.Generic;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Coin.Core;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class BindPhoneEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(BindPhoneEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public BindPhoneEvent(string phoneNumber, string countryCode)
        {
            this.PhoneNumber = phoneNumber;
            this.CountryCode = countryCode;
        }
        public BindPhoneEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.PhoneNumber = PhoneNumber;
                modelState.CountryCode = CountryCode;
                modelState.NeedSecondVerify = true;
            }
        }
    }
}
