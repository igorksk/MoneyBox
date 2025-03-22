namespace MoneyBox
{
    internal class Program
    {
        static void Main()
        {
            // Input initial amount, target amount, and monthly deposit
            decimal initialAmount = GetValidInput("Enter initial amount: ");
            decimal monthlyDeposit = GetValidInput("Enter monthly deposit: ");
            decimal targetAmount = GetValidInput("Enter target amount: ");

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

            // Output the result
            Console.WriteLine($"You will need {monthsToReachGoal} months to reach the target.");
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
                    Console.WriteLine("Invalid input. Please enter a valid positive number.");
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
