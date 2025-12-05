using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTracking_1
{
    public class AssetList
    {
        private readonly List<Asset> _assets;

        public AssetList()
        {
            _assets = new List<Asset>();
        }
        public void AddAsset(Asset asset)
        {
            _assets.Add(asset);
        }
        public IReadOnlyList<Asset> GetAssetssList()
        {
            return _assets.AsReadOnly();
        }

        public bool IsEmpy => !_assets.Any();
        private PrintHelper PrintHelper = new PrintHelper();
       public void PrintList()
{
    Console.WriteLine($"{"Type",-15}{"Brand",-10}{"Model",-10}{"Office",-10}{"Purchase Date",-20}" +
        $"{"Price in USD",-20}{"Currency",-15}{"Local price today",-20}");
    Console.WriteLine($"{"----",-15}{"-----",-10}{"-----",-10}{"------",-10}{"-------------",-20}" +
        $"{"------------",-20}{"--------",-15}{"-----------------",-20}");

    foreach (var p in _assets
        .OrderBy(x => x.Office.Trim())            
        .ThenBy(x => x.PurchaseDate))
    {
        var isType = p.GetType().Name;
        var brand = (p.Brand);
        var model = (p.ModelName);
        var office = (p.Office);
        var purchaseDate = p.PurchaseDate.ToString("yyyy-MM-dd");
        var priceFormatted = p.PurchasePriceUSD;
        var currency = PrintHelper.LocationCurrency(p);
        var local = PrintHelper.LocalPrice(p);

        var line = string.Format("{0,-15}{1,-10}{2,-10}{3,-10}{4,-20}{5,-20}{6,-15}{7,-20}",
            isType, brand, model, office, purchaseDate, priceFormatted, currency, local);

        var signal = p.EndOfLifeSignaling();
        if (signal == "Red") { PrintHelper.InRed(line); continue; }
        if (signal == "Yellow") { PrintHelper.InYellow(line); continue; }
        PrintHelper.InWhite(line);
    }
}

    }
}
