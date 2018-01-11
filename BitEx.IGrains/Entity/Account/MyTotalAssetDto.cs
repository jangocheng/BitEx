using System.Collections.Generic;

namespace BitEx.IGrain.Entity
{
    public class MyTotalAssetDto
    {
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public List<CurrencyAsset> AssetList { get; set; } = new List<CurrencyAsset>();
    }
}
