using System;

namespace AssetTracking_1
{
    public class Laptop : Asset
    {
        public string SerialNr { get; }

        public Laptop(string brand, string modelName, string office, DateTime purchaseDate, decimal purchasePriceUSD
            , string serialNr) : base(brand, modelName, office, purchaseDate, purchasePriceUSD)
        {
            SerialNr = serialNr;
        }
    }
}
