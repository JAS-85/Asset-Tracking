using System;

namespace AssetTracking_1
{
    public abstract class Asset
    {
        private static int NextId = 1;

        public int Id { get; }

        public string Brand { get; }
        public string ModelName { get; }

        public string Office { get; set; }

        public DateTime PurchaseDate { get; }

        public decimal PurchasePriceUSD { get; }

        protected Asset(string brand, string modelName, string office, DateTime purchaseDate, decimal purchasePriceUSD)
        {
            Id = NextId++;
            Brand = brand;
            ModelName = modelName;
            Office = office;
            PurchaseDate = purchaseDate;
            PurchasePriceUSD = purchasePriceUSD;
        }

        public int MonthsSincePurchase()
        {
            var today = DateTime.Now.Date;
            return (today.Year - PurchaseDate.Year) * 12 + (today.Month - PurchaseDate.Month);
        }

        public int MonthsUntilEndOfLife()
        {
            int lifeMonths = 36;
            return (lifeMonths - MonthsSincePurchase());
        }

        public string EndOfLifeSignaling()
        {
            int monthsLeft = MonthsUntilEndOfLife();
            if (monthsLeft < 3) return "Red";
            if (monthsLeft < 6) return "Yellow";
            else return "White";
        }
    }
}
