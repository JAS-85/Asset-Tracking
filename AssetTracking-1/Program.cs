// See https://aka.ms/new-console-template for more information
using AssetTracking_1;
using System.Globalization;
using System.Linq;

var assetList = new AssetList();
assetList.PrintList();

while (true)
{
    AddAsset(assetList);
    assetList.PrintList();
    ChooseAction(assetList);
}

static void AddAsset(AssetList assetList)
{
    while (true)
    {
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("To enter a new asset follow the steps | To quit - enter: Q");

        Console.Write("Enter Brand: ");
        var brand = Console.ReadLine();
        if (CheckQuit(brand)) break;
        if (string.IsNullOrWhiteSpace(brand) || brand.Length > 15)
        {
            Console.WriteLine("Brand cannot be empty or more than 15 chars");
            continue;
        }

        Console.Write("Enter Model Name: ");
        var model = Console.ReadLine();

        if (CheckQuit(model)) break;

        if (string.IsNullOrWhiteSpace(model) || model.Length > 20)
        {
            Console.WriteLine("Model cannot be empty or more than 20 chars");
            continue;
        }

        Console.Write("Enter Office (Sweden / Spain / USA): ");
        var officeRaw = Console.ReadLine();

        if (CheckQuit(officeRaw)) break;

        if (string.IsNullOrWhiteSpace(officeRaw) || officeRaw.Length > 20)
        {
            Console.WriteLine("Office cannot be empty or more than 20 chars");
            continue;
        }
        var officeNorm = officeRaw.Trim();
        if (string.Equals(officeNorm, "sweden", StringComparison.OrdinalIgnoreCase)) officeNorm = "Sweden";

        else if (string.Equals(officeNorm, "spain", StringComparison.OrdinalIgnoreCase)) officeNorm = "Spain";

        else if (string.Equals(officeNorm, "usa", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(officeNorm, "us", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(officeNorm, "united states", StringComparison.OrdinalIgnoreCase))
            officeNorm = "USA";


        else
        {
            Console.WriteLine("Office must be Sweden, Spain or USA (not case sensitive).");
            continue;
        }

        Console.Write("Enter Purchase Date (yyyy-MM-dd) (no earlier than 2000-01-01): ");
        var dateInput = Console.ReadLine();

        if (CheckQuit(dateInput)) break;

        if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var purchaseDate))
        {
            Console.WriteLine("Invalid date. Use format yyyy-MM-dd");
            continue;
        }

        if (purchaseDate.Date < new DateTime(2000, 1, 1))
        {
            Console.WriteLine("Purchase date cannot be earlier than 2000-01-01.");
            continue;
        }

        Console.Write("Enter price (USD): ");
        var priceInput = Console.ReadLine();
        if (CheckQuit(priceInput)) break;

        if (!decimal.TryParse(priceInput, out var price) || price < 0)
        {
            Console.WriteLine("Invalid price. Enter a non-negative number.");
            continue;
        }

        Console.Write("Enter Serial (if laptop) - leave blank for mobile phone: ");
        var serial = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(serial))
        {
            Console.Write("Enter Carrier (mobile) : ");
            var carrier = Console.ReadLine();
            if (CheckQuit(carrier)) break;
            if (string.IsNullOrWhiteSpace(carrier) || carrier.Length > 30)
            {
                Console.WriteLine("Carrier cannot be empty or more than 30 chars");
                continue;
            }

            var mobile = new MobilePhone(brand.Trim(), model.Trim(), officeNorm, purchaseDate.Date, price, carrier.Trim());
            assetList.AddAsset(mobile);
        }
        else
        {
            var laptop = new Laptop(brand.Trim(), model.Trim(), officeNorm, purchaseDate.Date, price, serial.Trim());
            assetList.AddAsset(laptop);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The asset was successfully added!");
        Console.ResetColor();
        break;
    }
}

static bool CheckQuit(string? input)
{
    return input != null && input.Trim().ToLower() == "q";
}

static void ChooseAction(AssetList assetList)
{
    while (true)
    {
        Console.WriteLine("To enter a new asset - enter: a | To search - enter: s | To quit - enter: q");
        Console.ResetColor();

        var chosenAction = (Console.ReadLine() ?? "").Trim().ToLower();
        if (chosenAction == "q") Environment.Exit(0);
        if (chosenAction == "a") return;
        if (chosenAction == "s")
        {
            Console.WriteLine("Enter search string (Model or Brand):");
            var find = (Console.ReadLine() ?? "").Trim();
            var found = assetList.GetAssetssList().FirstOrDefault(p =>
                string.Equals(p.ModelName, find, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(p.Brand, find, StringComparison.OrdinalIgnoreCase));

            if (found != null)
            {
                var ph = new PrintHelper();
                ph.InWhite("Found:");
                ph.InWhite($"{found.GetType().Name,-15}{found.Brand,-10}{found.ModelName,-10}{found.Office,-10}" +
                    $"{found.PurchaseDate:yyyy-MM-dd,-20}{found.PurchasePriceUSD,-20}{ph.LocationCurrency(found),-15}{ph.LocalPrice(found),-20}");
            }

            else
            {
                Console.WriteLine("Not found or wrong entry");
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(intercept: true);
            return;
        }

        Console.WriteLine("Enter a valid option");
    }
}
