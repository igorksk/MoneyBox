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
            
            // Validate the monthly deposit
            if (monthlyDeposit <= 0)
            {
                Console.WriteLine("Monthly deposit must be greater than zero.");
                return;
            }

            // Calculate the remaining amount to reach the goal
            decimal remainingAmount = targetAmount - initialAmount;

            // Check if the initial amount is already greater than or equal to the goal
            if (remainingAmount <= 0)
            {
                Console.WriteLine("Your initial amount is already greater than or equal to the target!");
                return;
            }

            // Calculate the number of months required to reach the goal
            int monthsToReachGoal = (int)Math.Ceiling(remainingAmount / monthlyDeposit);

            // Calculate the exact month and year when the target will be reached.
            // We take the current month and add the calculated months.
            // If your first monthly deposit is applied immediately (today), subtract 1 when monthsToReachGoal > 0.
            DateTime achievementDate = DateTime.Today.AddMonths(monthsToReachGoal);
            string achievementMonth = achievementDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture);

            // Output the result including the month and year
            Console.WriteLine($"You will need {monthsToReachGoal} months to reach the target (by {achievementMonth}).");
            Console.ReadLine();
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
    }
}
