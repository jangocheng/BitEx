using ProtoBuf;
using System;

namespace BitEx.IGrain.States
{
    public enum AccountStatus : byte
    {
        Actived = 1,
        Locked = 2,
        Disabled = 3
    }
    public enum CoinAccountStatus : byte
    {
        Actived = 1,
        Locked = 2,
        Disabled = 3
    }
    public enum WithdrawStatus : byte
    {
        None = 0,
        Started = 1,
        /// <summary>
        /// 已分配处理
        /// </summary>
        Assigned = 2,
        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 3,
        /// <summary>
        /// 提现完成
        /// </summary>
        Completed = 4,
        /// <summary>
        /// 提现失败
        /// </summary>
        Failed = 5,
        /// <summary>
        /// 提现取消
        /// </summary>
        Canceled = 6,
        /// <summary>
        /// 用户自己撤销
        /// </summary>
        Repealed = 7
    }
    public enum PayWay : byte
    {
        Bank = 1,
        Alipay = 2,
        Tenpay = 3,
        DepositCode = 4,
        YSBPay = 5
    }
    public enum DepositWay : byte
    {
        Bank = 1,
        Alipay = 2,
        Tenpay = 3,
        DepositCode = 4,
        YSBPay = 5
    }
    public enum DepositStatus : byte
    {
        None = 0,
        Started = 1,
        Completed = 2,
        /// <summary>
        /// 过期-失效（针对充值提交超过3天，仍未付款的人民币充值）
        /// </summary>
        Invalid = 3,
        Failed = 4,
        Canceled = 5
    }
    public enum DepositCodeStatus
    {
        None = 0,
        Actived = 1,
        Used = 2,
        Destroyed = 3
    }
    public enum TransferType : byte
    {
        /// <summary>
        /// 转入
        /// </summary>
        In = 1,
        /// <summary>
        /// 投资理财
        /// </summary>
        OutInvest = 2,
        /// <summary>
        /// 转出,硬性损失，主要是赔偿或是硬性损失
        /// </summary>
        OutHard = 3
    }
    public enum CoinTransferType : byte
    {
        /// <summary>
        /// 从冷钱包转入
        /// </summary>
        ColdToHot = 1,
        /// <summary>
        /// 转出到冷钱包
        /// </summary>
        HotToCold = 2,
        /// <summary>
        /// 硬性支出，主要是赔偿或是硬性损失
        /// </summary>
        OutHard = 3
    }
    public enum OrderCategory : byte
    {
        Order = 1,
        PlanOrder = 2
    }
    public enum OrderType : byte
    {
        None = 0,
        /// <summary>
        /// 买单
        /// </summary>
        Bid = 1,
        /// <summary>
        /// 卖单
        /// </summary>
        Ask = 2
    }
    public enum OrderSource : byte
    {
        WebSite = 1,
        IosMobile = 2,
        AndroidMobile = 3,
        Api = 4
    }
    public enum OrderStatus : byte
    {
        None = 0,
        Open = 1,
        Done = 2,
        Canceled = 3
    }
    [Flags]
    public enum MarketStatus : byte
    {
        /// <summary>
        /// 正在进行交易
        /// </summary>
        Opening = 1,
        /// <summary>
        /// 市场锁定
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 市场关闭
        /// </summary>
        Closed = 4
    }
    public enum MarketArea : byte
    {
        A = 1,
        B = 2,
        C = 3
    }
    public enum WithdrawValidResult : byte
    {
        None = 0,
        OverDailyLimit = 1,
        OverMonthlyLimit = 2,
        ManualVerifyLine = 4
    }
    public enum CoinWithdrawStatus : byte
    {
        None = 0,
        Started = 1,
        Pending = 2,
        Processing = 3,
        Completed = 4,
        CreateFailed = 5,
        ProcessingFailed = 6,
        Canceled = 7,
        Unknown = 8
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    [System.Flags]
    public enum CoinDepositStatus : byte
    {
        None = 0,
        /// <summary>
        /// 提交申请成功，提交充值后，就进入这个状态
        /// </summary>
        Increased = 1,
        /// <summary>
        /// 已经成功确认
        /// </summary>
        Confirmed = 2,
        /// <summary>
        /// 抵押充值，抵押开始
        /// </summary>
        GuaranteeDeposited = 4,
        /// <summary>
        /// 抵押充值，抵押成功
        /// </summary>
        GuaranteeSuccessed = 8,
        /// <summary>
        /// 抵押充值，抵押失败
        /// </summary>
        GuaranteeFailed = 16,
        /// <summary>
        /// 充值达到安全次数
        /// </summary>
        Safe = 32,
        /// <summary>
        /// 充值取消
        /// </summary>
        Canceled = 64
    }

    public enum CoinDepositSpecialStatus : byte
    {
        None = 0,
        /// <summary>
        /// 提交申请成功，提交充值后，就进入这个状态
        /// </summary>
        Increased = 1,
        /// <summary>
        /// Bts memo异常的订单
        /// </summary>
        Confirmed = 2,
        /// <summary>
        /// 充值适配
        /// </summary>
        Canceled = 3,
        /// <summary>
        /// 不适配
        /// </summary>
    }

    public enum TradeStatus : byte
    {
        UnComplete = 1,
        Completed = 2
    }
    public enum VipFeeType : byte
    {
        Withdraw = 1,
        Deposit = 2,
        Trade = 3
    }

    public enum ModifyFlag : byte
    {
        None = 0,
        Created = 1,
        Modified = 2
    }
    public enum VerifyWay : byte
    {
        Otp = 1,
        Phone = 2
    }
    /// <summary>
    /// 价格浮动
    /// </summary>
    public enum PriceTrend : byte
    {
        /// <summary>
        /// 上涨
        /// </summary>
        Risen = 1,
        /// <summary>
        /// 下跌
        /// </summary>
        Fallen = 2,
        /// <summary>
        /// 持平
        /// </summary>
        Flat = 3
    }
    public enum AccountLockReason : byte
    {
        None = 0,
        DepositTerminated = 1,
        ManualLock = 2
    }
    public enum GuaranteeStatus : byte
    {
        /// <summary>
        /// 抵押申请成功
        /// </summary>
        Successed = 1,
        /// <summary>
        /// 抵押已赎回
        /// </summary>
        Refunded = 2,
        /// <summary>
        /// 取消抵押
        /// </summary>
        Canceled = 3,
        /// <summary>
        /// 抵押申请失败
        /// </summary>
        Failed = 4
    }

    public enum BazaarPublishStatus : byte
    {
        /// <summary>
        /// 进行中
        /// </summary>
        Processing = 1,
        /// <summary>
        /// 已完成
        /// </summary>
        Successed = 2,
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3,
        /// <summary>
        /// 取消
        /// </summary>
        Canceled = 4
    }
}
