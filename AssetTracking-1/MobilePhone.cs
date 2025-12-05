using System;

namespace AssetTracking_1
{
    internal class MobilePhone : Asset
    {
        public string Carrier { get; set; }

        public MobilePhone(string brand, string modelName, string office, DateTime purchaseDate, decimal purchasePriceUSD
            , string carrier) : base(brand, modelName, office, purchaseDate, purchasePriceUSD)
        {
            Carrier = carrier;
        }
    }
}
