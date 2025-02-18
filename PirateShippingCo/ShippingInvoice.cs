// ShippingInvoice.cs
using System;

namespace PirateShippingCo
{
    class ShippingInvoice
    {
        // Base shipping rate per ton
        private const decimal BasePricePerTon = 220.40m;

        // Additional charge for shipping perishable goods
        private const decimal PerishableCharge = 230.00m;

        // Extra fee for express shipping (25% of the subtotal)
        private const decimal ExpressMultiplier = 0.25m;

        public static void GenerateInvoice()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("  Shipping Invoice");
            Console.WriteLine("====================================");

            // Ask user for weight and the unit they are using
            Console.Write("Enter weight unit (tons/kg/lb): ");
            string unit = Console.ReadLine().ToLower();

            Console.Write("Enter weight: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal inputWeight) || inputWeight <= 0)
            {
                Console.WriteLine("Invalid weight entered! Press Enter to return.");
                Console.ReadLine();
                return;
            }

            // Convert weight into tons for consistency
            decimal tons = ConvertToTons(inputWeight, unit);
            if (tons <= 0)
            {
                Console.WriteLine("Invalid unit entered! Please use tons, kg, or lb.");
                Console.ReadLine();
                return;
            }

            // Ask whether the shipment contains perishables
            Console.Write("Is the shipment perishable? (Y/N): ");
            bool perishable = Console.ReadLine().Trim().ToUpper() == "Y";

            // Ask whether the user wants express shipping
            Console.Write("Do you need express shipping? (Y/N): ");
            bool express = Console.ReadLine().Trim().ToUpper() == "Y";

            // Calculate total cost and display the breakdown
            var (total, breakdown) = CalculateCosts(tons, perishable, express);
            Console.WriteLine("\n=== Invoice Breakdown ===");
            Console.WriteLine(breakdown);
            Console.WriteLine($"Total Cost: {total:C2}");

            // Proceed with payment process
            ProcessPayment(total);
        }

        
        // Converts the given weight into tons based on the selected unit.      
        private static decimal ConvertToTons(decimal weight, string unit)
        {
            return unit switch
            {
                "tons" => weight,
                "kg" => weight / 1000m,       // 1 ton = 1000 kg
                "lb" => weight / 2204.62m,    // 1 ton ≈ 2204.62 lbs
                _ => -1m                      // Invalid unit
            };
        }

        // Calculate the shipping costs based on weight, perishable status, and express shipping.        
        private static (decimal total, string breakdown) CalculateCosts(decimal tons, bool perishable, bool express)
        {
            // Calculate base cost
            decimal baseCost = tons * BasePricePerTon;

            // Add perishable charge if applicable
            decimal perishCost = perishable ? tons * PerishableCharge : 0;

            // Calculate subtotal before express fee
            decimal subtotal = baseCost + perishCost;

            // Apply express fee if selected (25% of subtotal)
            decimal expressFee = express ? subtotal * ExpressMultiplier : 0;

            // Final total amount
            decimal total = subtotal + expressFee;

            // Create a detailed breakdown of the costs
            string breakdown =
                $"Base Cost: {BasePricePerTon:C2}/ton × {tons:F2} tons = {baseCost:C2}\n" +
                (perishable ? $"Perishables Fee: {PerishableCharge:C2}/ton × {tons:F2} tons = {perishCost:C2}\n" : "") +
                $"Subtotal: {subtotal:C2}\n" +
                (express ? $"Express Fee: 25% of {subtotal:C2} = {expressFee:C2}\n" : "");

            return (total, breakdown);
        }

       
        // Handles the payment process, converting from foreign currencies if needed.       
        private static void ProcessPayment(decimal totalDue)
        {
            while (true)
            {
                // Ask user for the currency they will use to pay
                Console.Write("\nEnter payment currency (e.g., USD, GBP, JPY, etc.): ");
                string currency = Console.ReadLine().ToUpper();

                // Check if the currency is supported
                if (!CurrencyConverter.IsValidCurrency(currency))
                {
                    Console.WriteLine("Sorry, we do not accept this currency.");
                    continue;
                }

                // Ask for the payment amount
                Console.Write($"Enter amount paid in {currency}: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal paid) || paid < 0)
                {
                    Console.WriteLine("Invalid amount entered!");
                    continue;
                }

                // Convert the entered amount to USD
                decimal paidUSD = CurrencyConverter.ConvertCurrencyAmount(currency, "USD", paid);

                // Check if payment is enough
                if (paidUSD < totalDue)
                {
                    Console.WriteLine($"Insufficient funds! You need {totalDue:C2} USD but provided {paidUSD:C2} USD.");
                    continue;
                }

                // Calculate and display change if overpaid
                decimal change = paidUSD - totalDue;
                Console.WriteLine($"Payment successful! Your change: {change:C2} USD");
                Console.ReadLine();
                return;
            }
        }
    }

}
