using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class BindEmailEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(BindEmailEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Email { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public BindEmailEvent(string email)
        {
            this.Email = email;
        }
        public BindEmailEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Email = Email;
                if (!modelState.IsPhoneRegistered && modelState.Status == UserStatus.NoActive)
                {
                    modelState.Status = UserStatus.Active;
                }
            }
        }
    }
}
