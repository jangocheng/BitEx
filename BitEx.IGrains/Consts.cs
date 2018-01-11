namespace BitEx.IGrain
{
    public class Consts
    {
        public const string AdminUserId = "Admin";
        public const string AdminAccountId = "Admin";
        public const int AdminManagerId = 0;
        public const string CoinMonitorExchange = "Coin.Monitor";
        public const string CoinMonitorQueue = "Coin.Monitor.Currency_";
        public const string DepositIdentifyKey = "DepositV2";
        public const int DepositMinIdentify = 300000;
        public const ushort KLineCommand = 901;
        public const string KLineExchange = "KLine";
        public const string KLineRoutingKey = "KLine";
        public const ushort MarketDepthCommand = 904;
        public const string MarketDepthExchange = "MarketDepth";
        public const string MarketDepthRoutingKey = "MarketDepth";
        public const string AdminNoticeEmailListKey = "AdminNoticeEmailList";      
    }
}
