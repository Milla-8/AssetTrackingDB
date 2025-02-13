using Mini_Project_v49_Asset_Tracking;
using System.Globalization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackingDB
{
    public class AssetController
	{
        public AssetController()
		{
        }
        public static void ShowAssetList()
        {

            List<Asset> AssetList = Asset.getAssetList();

            //// Using dynamic column width since length of input can differ too much between assets
            int idWidth = AssetList.Max(a => a.AssetId.ToString().Length) + 2;  // Add 2 for space
            int officeWidth = AssetList.Max(a => a.Office.City.Length) + 2;  // Add 2 for space
            int categoryWidth = AssetList.Max(a => a.Category.Length) + 2;
            int brandWidth = AssetList.Max(a => a.Brand.Length) + 2;
            int modelWidth = AssetList.Max(a => a.Model.Length) + 2;
            int configWidth = AssetList.Max(a => a.Configuration.Length) + 2;
            int dateWidth = 18; // Setting a fixed width for purchase date since the header name is longer than the actual date
            int priceWidth = 12; // Setting a fixed width for price since it should be almost always the same length
            int currencyWidth = 10;

            // Using dynamic width for column header also
            Console.WriteLine(
                $"{"ID".PadRight(idWidth)}" +
                $"{"Office".PadRight(officeWidth)}" +
                $"{"Category".PadRight(categoryWidth)}" +
                $"{"Brand".PadRight(brandWidth)}" +
                $"{"Model".PadRight(modelWidth)}" +
                $"{"Configuration".PadRight(configWidth)}" +
                $"{"PurchaseDate".PadRight(dateWidth)}" +
                $"{"Price".PadRight(priceWidth)}" +
                $"{"Currency".PadRight(currencyWidth)}");

            // Print line to separate header from list data
            Console.WriteLine(new string('-', idWidth + officeWidth + categoryWidth + brandWidth + modelWidth + configWidth + dateWidth + priceWidth + currencyWidth));

            foreach (Asset asset in AssetList.OrderBy(x => x.Office.City).ThenBy(x => DateTime.ParseExact(x.PurchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
            {
                DateTime purchaseDate = DateTime.ParseExact(asset.PurchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime warrantyEndDate = purchaseDate.AddYears(3);

                SetConsoleColors(warrantyEndDate);

                // Print the asset data from the list using dynamic width
                Console.Write(
                    $"{asset.AssetId.ToString().PadRight(idWidth)}" +
                    $"{asset.Office.City.PadRight(officeWidth)}" +
                    $"{asset.Category.PadRight(categoryWidth)}" +
                    $"{asset.Brand.PadRight(brandWidth)}" +
                    $"{asset.Model.PadRight(modelWidth)}" +
                    $"{asset.Configuration.PadRight(configWidth)}" +
                    $"{asset.PurchaseDate.PadRight(dateWidth)}" +
                    $"{asset.Price.ToString("F2").PadRight(priceWidth)}" +
                    $"{asset.Office.Currency.ShortName.PadRight(currencyWidth)}");
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', idWidth + officeWidth + categoryWidth + brandWidth + modelWidth + configWidth + dateWidth + priceWidth + currencyWidth));
        }
        private static void SetConsoleColors(DateTime warrantyEndDate) // Set console colors based on warranty end date
        {
            if (warrantyEndDate.AddMonths(-3) < DateTime.Now)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (warrantyEndDate.AddMonths(-6) < DateTime.Now)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
       public static void AddNewAsset()
        {
            MyDbContext context = new MyDbContext();

            while (true)
            {
                Asset asset = new Asset();

                Console.Write("Type in the category: ");
                asset.Category = Console.ReadLine();

                Console.Write("Type in the brand: ");
                asset.Brand = Console.ReadLine();

                Console.WriteLine("Type in the model (product name): ");
                asset.Model = Console.ReadLine();

                Console.Write("Type in the configuration: ");
                asset.Configuration = Console.ReadLine();

                asset.PurchaseDate = GetValidPurchaseDate();

                asset.Price = GetValidPrice();

                asset.OfficeId = GetValidOfficeId();

                try
                {
                    context.Assets.Add(asset);
                    context.SaveChanges();
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Failed to save Product in database.");
                    Console.ResetColor();
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Product successfully added!");
                    Console.ResetColor();
                }

                Console.WriteLine("Would you like to add more products? Y/N");

                if (Helpers.Simplify(Console.ReadLine()) == "N")
                {
                    ShowAssetList();
                    break;
                }
            }
        }

    private static string GetValidPurchaseDate()
        {
            while (true)
            {
                Console.WriteLine("Type in the purchase date in the following format YYYY-MM-DD: ");
                string purchaseDate = Console.ReadLine();

                if (Helpers.ValidateDate(purchaseDate))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("This is a valid date!");
                    Console.ResetColor();
                    return purchaseDate;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to enter the purchase date in the following format YYYY-MM-DD ");
                Console.ResetColor();
            }
        }

        private static float GetValidPrice()
        {
            while (true)
            {
                Console.WriteLine("Type in the price in the following format 0.00: ");
                string input = Console.ReadLine().Replace(',', '.');

                if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out float price) &&
                    Math.Abs(price * 100 - Math.Round(price * 100)) < 0.01)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("This is a valid price!");
                    Console.ResetColor();
                    return price;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to enter a numeric value in format 0.00: ");
                Console.ResetColor();
            }
        }

        private static int GetValidOfficeId()
        {
            List<Office> OfficeList = Office.getOfficeList();

            while (true)
            {
                Console.WriteLine("Type in which office this belongs to (assign ID): ");

                foreach (Office office in OfficeList)
                {
                    Console.WriteLine($"{office.OfficeId} - {office.City}");
                }

                if (int.TryParse(Console.ReadLine(), out int officeChoice) && OfficeList.Any(o => o.OfficeId == officeChoice))
                {
                    return officeChoice;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to enter an existing office value: ");
                Console.ResetColor();
            }
        }

        private static int GetValidAssetId()
        {
            List<Asset> AssetList = Asset.getAssetList();

            while (true)
            {
                ShowAssetList();
                Console.WriteLine("Type in the ID of the asset: ");

                if (int.TryParse(Console.ReadLine(), out int assetChoice) && AssetList.Any(a => a.AssetId == assetChoice))
                {
                    return assetChoice;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to enter an existing asset value: ");
                Console.ResetColor();
            }
        }
    
        public static void UpdateAsset()
        {
            MyDbContext context = new MyDbContext();

            while (true)
            {
                int assetId = GetValidAssetId();
                Asset asset = context.Assets.Find(assetId);

                Console.Write("Type in the category: ");
                asset.Category = Console.ReadLine();

                Console.Write("Type in the brand: ");
                asset.Brand = Console.ReadLine();

                Console.WriteLine("Type in the model (product name): ");
                asset.Model = Console.ReadLine();

                Console.Write("Type in the configuration: ");
                asset.Configuration = Console.ReadLine();

                asset.PurchaseDate = GetValidPurchaseDate();

                asset.Price = GetValidPrice();

                asset.OfficeId = GetValidOfficeId();

                try
                {
                    context.Assets.Update(asset);
                    context.SaveChanges();
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Failed to save Product in database.");
                    Console.ResetColor();
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Product successfully updated!");
                    Console.ResetColor();
                }

                Console.WriteLine("Would you like to update more products? Y/N");

                if (Helpers.Simplify(Console.ReadLine()) == "N")
                {
                    ShowAssetList();
                    break;
                }
            }
        }

        public static void RemoveAsset()
        {
            MyDbContext context = new MyDbContext();

            while (true)
            {
                int assetId = GetValidAssetId();
                Asset asset = context.Assets.Find(assetId);

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Are you sure you want to remove this product?");
                Console.ResetColor();
                string choice = Helpers.Simplify(Console.ReadLine());
                if (choice != "Y")
                {
                    continue;
                }

                try
                {
                    context.Assets.Remove(asset);
                    context.SaveChanges();
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Failed to remove Product from database.");
                    Console.ResetColor();
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Product successfully removed!");
                    Console.ResetColor();
                }

                Console.WriteLine("Would you like to remove more products? Y/N");

                if (Helpers.Simplify(Console.ReadLine()) == "N")
                {
                    ShowAssetList();
                    break;
                }
            }
        }
    }
}



