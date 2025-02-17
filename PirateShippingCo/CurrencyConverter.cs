// CurrencyConverter.cs
using System;
using System.Collections.Generic;

namespace PirateShippingCo
{
    class CurrencyConverter
    {
        // Exchange rates and currency symbols
        private static readonly Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>
        {
            {"JPY", 0.0064m}, {"CNY", 0.14m}, {"GBP", 1.22m}, 
            {"DBL", 8.40m}, {"DZD", 0.0074m}
        };

        private static readonly Dictionary<string, string> currencySymbols = new Dictionary<string, string>
        {
            {"USD", "$"}, {"JPY", "¥"}, {"CNY", "¥"}, 
            {"GBP", "£"}, {"DBL", "♦"}, {"DZD", "د.ج"}
        };

        public static void ConvertCurrency()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("  Currency Conversion ");
            Console.WriteLine("====================================");

            Console.WriteLine("Supported Currencies: USD, JPY, CNY, GBP, DBL, DZD");

            // Get conversion details
            Console.Write("Enter currency to convert FROM (e.g., USD, JPY): ");
            string from = Console.ReadLine().ToUpper();
            if (!IsValidCurrency(from)) return;

            Console.Write("Enter currency to convert TO (e.g., USD, GBP): ");
            string to = Console.ReadLine().ToUpper();
            if (!IsValidCurrency(to)) return;

            Console.Write("Enter the amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid doubloons! Press Enter to return.");
                Console.ReadLine();
                return;
            }

            // Perform conversion and show with symbols
            decimal result = ConvertCurrencyAmount(from, to, amount);
            string fromSymbol = GetCurrencySymbol(from);
            string toSymbol = GetCurrencySymbol(to);
            Console.WriteLine($"\n{fromSymbol}{amount:N2} = {toSymbol}{result:N2}");

            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine();
        }

        // Helper to get currency symbol
        private static string GetCurrencySymbol(string code) => 
            currencySymbols.TryGetValue(code, out string symbol) ? symbol : code;

        // Public validation check
        public static bool IsValidCurrency(string code) => 
            code == "USD" || exchangeRates.ContainsKey(code);

        // Public conversion method
        public static decimal ConvertCurrencyAmount(string from, string to, decimal amount)
        {
            decimal usdAmount = from == "USD" ? amount : amount * exchangeRates[from];
            return to == "USD" ? usdAmount : usdAmount / exchangeRates[to];
        }
    }
}