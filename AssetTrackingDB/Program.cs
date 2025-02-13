using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Runtime.ConstrainedExecution;
using AssetTrackingDB;
using Microsoft.EntityFrameworkCore;


namespace Mini_Project_v49_Asset_Tracking
{
    class Program
    {
        MyDbContext context = new MyDbContext();

        public static bool runProgram = true;

        static void Main(string[] args)
        {
            while (runProgram)
            {
                Helpers.PrintMenu(); // Shows menu with choices for user to perform

                switch (Helpers.Simplify(Console.ReadLine())) // Based on user's choice - do different things

                {
                    case "1":

                        AssetController.AddNewAsset(); // Adds new asset to the list
                        break;

                    case "2":
                        AssetController.UpdateAsset();// Update existing asset
                        break;

                    case "3":
                        AssetController.RemoveAsset(); // Remove existing asset
                        break;

                    case "4":
                        AssetController.ShowAssetList(); // Shows the complete list with all assets
                        break;

                    case "5":

                        Helpers.ExitProgram(); // Closes the program
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You must enter a number between 1-5");
                        Console.ResetColor();
                        break;
                }

            }

        }

    }
}


