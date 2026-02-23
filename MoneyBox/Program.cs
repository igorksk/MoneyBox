using System.Globalization;

namespace MoneyBox
{
    internal class Program
    {
        static void Main()
        {
            // Input initial amount, target amount, and monthly deposit
            decimal initialAmount = GetValidInput("Enter initial amount (example: 1000 or 1000.50): ");
            decimal targetAmount = GetValidInput("Enter target amount (example: 5000 or 5000.00): ");
            decimal monthlyDeposit = GetValidInput("Enter monthly deposit (example: 250 or 250.00): ");
            
            // Calculate months and achievement date (CalculateGoal returns an error message when appropriate)
            var (Months, AchievementDate, ErrorMessage) = CalculateGoal(initialAmount, monthlyDeposit, targetAmount);
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                Console.WriteLine(ErrorMessage);
                // Keep console open if run interactively so the user can read the error
                if (!Console.IsInputRedirected)
                {
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey(true);
                }
                return;
            }
            int monthsToReachGoal = Months;
            string achievementMonth = AchievementDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture);

            // Output the result including the month and year
            Console.WriteLine($"You will need {monthsToReachGoal} months to reach the target (by {achievementMonth}).");

            // If the app is run interactively (for example by double-clicking the exe), keep the console open until a key is pressed.
            // If input is redirected (for example in automated tests or piping), don't wait.
            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
            }
        }

        // Method to get valid input
        static decimal GetValidInput(string prompt)
        {
            decimal input;
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrEmpty(userInput) || !decimal.TryParse(userInput, out input) || input < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid non-negative number (for example: 1000 or 1000.50).\nUse a dot or your locale's decimal separator as appropriate.");
                }
                else
                {
                    break;
                }
            }
            return input;
        }

        // Pure, testable calculation method
        // Returns number of months required, the achievement date (calculated from today), and an optional error message
        internal static (int Months, DateTime AchievementDate, string ErrorMessage) CalculateGoal(decimal initialAmount, decimal monthlyDeposit, decimal targetAmount)
        {
            if (monthlyDeposit <= 0)
                return (0, DateTime.MinValue, "Monthly deposit must be greater than zero.");

            decimal remainingAmount = targetAmount - initialAmount;
            if (remainingAmount <= 0)
                return (0, DateTime.Today, "Your initial amount is already greater than or equal to the target.");

            int months = (int)Math.Ceiling(remainingAmount / monthlyDeposit);
            DateTime achievementDate = DateTime.Today.AddMonths(months);
            return (months, achievementDate, string.Empty);
        }
    }
}
