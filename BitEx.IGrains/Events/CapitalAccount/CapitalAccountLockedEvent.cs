﻿using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class CapitalAccountLockedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public int OperatorId { get; set; }
        public AccountStatus Status { get; set; }
        private static string _TypeCode = typeof(CapitalAccountLockedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CapitalAccountLockedEvent()
        {
        }
        public CapitalAccountLockedEvent(string commandId, int operatorId)
        {
            this.CommandId = commandId;
            this.OperatorId = operatorId;
            this.Status = AccountStatus.Locked;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CapitalAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Status = this.Status;
            }
        }
    }
}
