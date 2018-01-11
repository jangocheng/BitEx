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
    public class UserLockEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(UserLockEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public UserLockType LockType { get; set; }
        public UserStatus Status { get; set; }
        public string Remark { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public UserLockEvent(UserLockType type, UserStatus status, string remark)
        {
            this.LockType = type;
            this.Remark = remark;
            this.Status = status;
        }
        public UserLockEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.LockType = this.LockType;
                modelState.Status = this.Status;
            }
        }
    }
}
