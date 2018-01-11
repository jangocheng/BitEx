using System;
using Coin.Core.EventSourcing;
using BitEx.IGrain.States;
using ProtoBuf;
using BitEx.IGrain.Entity.User;
using Orleans.Concurrency;
using Coin.Core.Lib;
using Coin.Framework.ThirdParty;

namespace BitEx.IGrain.Events.User
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public class AddBankCardEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }

        public string StateId { get; set; }
        private static string _TypeCode = typeof(AddBankCardEvent).FullName;
        [ProtoIgnore]

        public string TypeCode
        {
            get
            {
                return _TypeCode;
            }
        }
        public BankType BankType { get; set; }
        public string Bank { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string BranchBank { get; set; }
        public string CardNumber { get; set; }
        public DateTime Timestamp { get; set; }

        public UInt32 Version { get; set; }
        public AddBankCardEvent(BankType bankType, string bank, string province, string city, string branchBank, string cardNumber)
        {
            this.Province = province;
            this.City = city;
            this.BankType = bankType;
            this.Bank = bank;
            this.BranchBank = branchBank;
            this.CardNumber = cardNumber;
        }
        public AddBankCardEvent() { }
        public void Apply(IState<string> state)
        {
            var modelState = state as UserState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                var bankCard = new BankCardInfo()
                {
                    Id = this.Id,
                    BankType = this.BankType,
                    Province = this.Province,
                    City = this.City,
                    CardNumber = this.CardNumber,
                    Bank = this.Bank,
                    BranchBank = this.BranchBank
                };
                modelState.BankCardList.Add(bankCard);
            }
        }
    }
}
