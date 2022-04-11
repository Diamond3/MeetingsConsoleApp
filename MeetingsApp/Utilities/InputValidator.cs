namespace MeetingsApp.Utilities
{
    internal class InputValidator
    {
        public static string StringInput(string? input)
        {
            while (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid string! Try again: ");
                Console.ResetColor();

                input = Console.ReadLine();
            }
            
            return input;
        }

        public static int NumberInput(string? input, int max = int.MaxValue)
        {
            var num = -1;
            while (!int.TryParse(input, out num) || (num < 0 || num > max))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Not a number or invalid input! Try again: ");
                Console.ResetColor();

                input = Console.ReadLine();
            }
            return num;
        }

        public static DateTime DateInput(string? input)
        {
            DateTime date;
            while (!DateTime.TryParse(input, out date))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid date! Try again: ");
                Console.ResetColor();

                input = Console.ReadLine();
            }
            return date;
        }
    }
}
