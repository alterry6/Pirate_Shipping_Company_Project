using System;

namespace PirateShippingCo
{
    class Utility
    {
        // Helper method to validate yes/no user input
        public static bool GetYesOrNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToUpper();
                if (input == "Y") return true;
                if (input == "N") return false;
                Console.WriteLine("Invalid input. Enter 'Y' or 'N'.");
            }
        }
    }
}
