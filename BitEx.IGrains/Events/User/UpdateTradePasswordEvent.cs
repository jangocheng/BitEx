﻿using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class UpdateTradePasswordEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UpdateTradePasswordEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public string Password { get; set; }
        public bool IsForget { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UpdateTradePasswordEvent(string password, bool isForgot = false)
        {
            this.Password = password;
            this.IsForget = isForgot;
        }
        public UpdateTradePasswordEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.TradePassword = this.Password;
                if (IsForget)
                    modelState.ForgotTradePasswordTime = this.Timestamp;
            }
        }
    }
}