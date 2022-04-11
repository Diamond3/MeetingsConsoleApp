namespace MeetingsApp.Utilities
{
    public static class Utils
    {
        public enum Category { CodeMonkey, Hub, Short, TeamBuilding }

        public enum Type { Live, InPerson }

        public static void WriteLineColor(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}
