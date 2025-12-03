using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AssetTracking_1
{
    abstract class Asset
    {
        private static int NextId = 1;

        public int Id { get; }

        public string ModelName {  get; }

        public DateTime PurchaseDate { get; }

        public int PurchasePriceUSD { get; }

        protected Asset(string modelName, DateTime purchaseDate, int purchasePriceUSD) 
        {
            Id = NextId++;
            ModelName = modelName;
            PurchaseDate = purchaseDate;
            PurchasePriceUSD = purchasePriceUSD;
        }

        public int MonthsSincePurchase()
        {
            var today = DateTime.Now;
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
            if (monthsLeft < 3) return "Red text";
            if (monthsLeft < 6) return "Yellow Yext";
            else return "White text";
        }
    }
}
