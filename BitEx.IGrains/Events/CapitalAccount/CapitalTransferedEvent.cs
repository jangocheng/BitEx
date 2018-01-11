using System;
using Coin.Core.EventSourcing;
using Coin.Framework.ThirdParty;
using BitEx.IGrain.States;
using ProtoBuf;

namespace BitEx.IGrain.Events
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CapitalTransferedEvent : IEventBase<string>
    {
        public string Id { get; set; }
        public string CommandId { get; set; }
        public string StateId { get; set; }
        public UInt32 Version { get; set; }
        public TransferType TransferType { get; set; }
        public string CurrencyId { get; set; }
        public string ToOwnerName { get; set; }
        public BankType ToAccountType { get; set; }
        public string ToSubbranch { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string TxNo { get; set; }
        public string Remark { get; set; }
        public int OperatorId { get; set; }
        private static string _TypeCode = typeof(CapitalTransferedEvent).FullName;
        [ProtoIgnore]
        public string TypeCode { get { return _TypeCode; } }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public CapitalTransferedEvent()
        {
        }
        public CapitalTransferedEvent(string commandId, TransferType txType, string currencyId, string toOwnerName, BankType toAccountType, string toSubbranch, string toAccountNumber, decimal balance, decimal amount, decimal fee, string txNo, string remark, int operatorId)
        {
            this.CommandId = commandId;
            this.TransferType = txType;
            this.CurrencyId = currencyId;
            this.ToOwnerName = toOwnerName;
            this.ToAccountType = toAccountType;
            this.ToSubbranch = toSubbranch;
            this.ToAccountNumber = toAccountNumber;
            this.CommandId = commandId;
            this.Balance = balance;
            this.Amount = amount;
            this.Fee = fee;
            this.TxNo = txNo;
            this.Remark = remark;
            this.OperatorId = operatorId;
        }
        public void Apply(IState<string> state)
        {
            var modelState = state as CapitalAccountState;
            if (modelState != null)
            {
                this.ApplyBase(modelState);
                modelState.Balance = this.Balance;
            }
        }
    }
}

