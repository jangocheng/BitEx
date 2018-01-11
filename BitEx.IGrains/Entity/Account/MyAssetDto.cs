using System;
using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class MyAssetListDto
    {
        public decimal WithdrawedAmount { get; set; }
        public List<MyAssetDto> AssetList { get; set; } = new List<MyAssetDto>();
    }
    public class MyAssetDto : MyAssetInfo
    {
        public List<CurrencyAsset> ExchangeList { get; set; } = new List<CurrencyAsset>();
        public static MyAssetDto From(MyAssetInfo asset)
        {
            return new MyAssetDto()
            {
                Id = asset.Id,
                UserName = asset.UserName,
                CurrencyId = asset.CurrencyId,
                CurrencyName = asset.CurrencyName,
                Address = asset.Address,
                IsVirtualCoin = asset.IsVirtualCoin,
                TotalAmount = asset.TotalAmount,
                Balance = asset.Balance,
                LockedAmount = asset.LockedAmount,
                MortgagedAmount = asset.MortgagedAmount,
                WithdrawOnceMin = asset.WithdrawOnceMin,
                WithdrawOnceLimit = asset.WithdrawOnceLimit,
                ShowPrecision = asset.ShowPrecision
            };
        }
    }
}
