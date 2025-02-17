using System;

namespace PirateShippingCo
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                // Clear the console and display the main menu
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("  Pirate Shipping Co.");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Convert Currency");
                Console.WriteLine("2. Shipping Invoice");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                
                // Read user input
                string choice = Console.ReadLine();
                
                // Process the user's menu choice
                switch (choice)
                {
                    case "1":
                        CurrencyConverter.ConvertCurrency();
                        break;
                    case "2":
                        ShippingInvoice.GenerateInvoice();
                        break;
                    case "3":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Press Enter to retry.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
