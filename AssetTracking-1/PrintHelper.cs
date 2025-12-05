using System;

namespace AssetTracking_1
{
    public class PrintHelper
    {
        public void InRed(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = prev;
        }
        public void InYellow(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = prev;
        }

        public void InWhite(string text)
        {
            Console.WriteLine(text);
        }

        public string LocationCurrency(Asset asset)
        {
            var office = asset.Office ?? "";
            if (string.Equals(office, "Spain", StringComparison.OrdinalIgnoreCase))
            {
                return "EUR";
            }
            if (string.Equals(office, "Sweden", StringComparison.OrdinalIgnoreCase))
            {
                return "SEK";
            }
            else
            {
                return "USD";
            }
        }

        public decimal LocalPrice(Asset asset)
        {
            decimal euro = 0.86m;
            decimal SEK = 9.41m;
            var office = asset.Office ?? "";

            if (string.Equals(office, "Spain", StringComparison.OrdinalIgnoreCase))
            {
                return asset.PurchasePriceUSD * euro;
            }
            if (string.Equals(office, "Sweden", StringComparison.OrdinalIgnoreCase))
            {
                return asset.PurchasePriceUSD * SEK;
            }
            else
            {
                return asset.PurchasePriceUSD;
            }
        }
    }
}
