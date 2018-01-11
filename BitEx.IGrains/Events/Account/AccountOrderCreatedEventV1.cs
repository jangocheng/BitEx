﻿using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using Orleans.Concurrency;
using Coin.Core.Lib;
using BitEx.IGrain.Entity.User;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AccountOrderCreatedEventV1 : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string OrderId { get; set; }
        public string MarketId { get; set; }
        public OrderType OrderType { get; set; }
        public OrderSource OrderSource { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal Locked { get; set; }
        public UserVipLevel VipLevel { get; set; }
        private static string _TypeCode = typeof(AccountOrderCreatedEventV1).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public AccountOrderCreatedEventV1()
        {
        }
        public AccountOrderCreatedEventV1(string commandId, string userId, UserVipLevel vipLevel, string currencyId, string marketId, OrderType orderType, OrderSource orderSource, decimal price, decimal volume, decimal amount, decimal balance, decimal locked)
        {
            this.OrderId = OGuid.GenerateNewId().ToString();
            this.CommandId = commandId;
            this.UserId = userId;
            this.VipLevel = vipLevel;
            this.CurrencyId = currencyId;
            this.MarketId = marketId;
            this.OrderType = orderType;
            this.OrderSource = orderSource;
            this.Price = price;
            this.Volume = volume;
            this.Amount = amount;
            this.Balance = balance;
            this.Locked = locked;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as AccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Balance = this.Balance;
                modelState.Locked = this.Locked;
                if (!modelState.ActivatedMarketList.Contains(MarketId))
                {
                    modelState.ActivatedMarketList.Add(MarketId);
                }
            }
        }
    }
}
