using MeetingsApp.Interfaces;
using MeetingsApp.Models;
using MeetingsApp.Utilities;

namespace MeetingsApp.Controlers
{
    internal class SearchMeetingsView : IView
    {
        private readonly IMeetingService _service;

        public SearchMeetingsView(IMeetingService service)
        {
            _service = service;
        }

        public void ShowTitle()
        {
            Console.WriteLine("Meeting search\n");
        }

        public void LoadBody()
        {
            var filterOptions = "0 - Go back\n" +
                "1 - Filter by description\n" +
                "2 - Filter by responsible person\n" +
                "3 - Filter by category\n" +
                "4 - Filter by type\n" +
                "5 - Filter by dates\n" +
                "6 - Filter by number of attendees\n" +
                "Choose option: ";

            while (true)
            {
                Console.Clear();
                ShowTitle();
                Console.Write(filterOptions);

                var option = InputValidator.NumberInput(Console.ReadLine(), 6);
                var meetings = new List<Meeting>();
                var input = "";
                var num = -1;

                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Write("\nSearch text: ");
                        input = InputValidator.StringInput(Console.ReadLine());
                        meetings = _service.GetMeetingsByDescription(input);

                        break;
                    case 2:
                        Console.Write("\nName of responsible person: ");
                        input = InputValidator.StringInput(Console.ReadLine());
                        meetings = _service.GetMeetingsByResponsiblePerson(new Person(input));

                        break;
                    case 3:
                        Console.Write("\nChoose category (0 - CodeMonkey | 1 - Hub | 2 - Short | 3 - TeamBuilding): ");
                        num = InputValidator.NumberInput(Console.ReadLine(), (int)Utils.Category.TeamBuilding);
                        meetings = _service.GetMeetingsByCategory((Utils.Category)num);

                        break;
                    case 4:
                        Console.Write("\nChoose type (0 - Live | 1 - InPerson): ");
                        num = InputValidator.NumberInput(Console.ReadLine(), (int)Utils.Type.InPerson);
                        meetings = _service.GetMeetingsByType((Utils.Type)num);

                        break;
                    case 5:
                        Console.Write("\nChoose type of search (0 - set from date | 1 - set from and to dates): ");
                        num = InputValidator.NumberInput(Console.ReadLine(), 1);
                        Console.Write("From date: ");
                        DateTime fromDate = InputValidator.DateInput(Console.ReadLine());

                        if (num == 1)
                        {
                            Console.Write("To date: ");
                            DateTime toDate = InputValidator.DateInput(Console.ReadLine());
                            meetings = _service.GetMeetingsByDates(fromDate, toDate);
                        }
                        else
                        {
                            meetings = _service.GetMeetingsByDates(fromDate, null);
                        }

                        break;
                    case 6:
                        Console.Write("\nChoose antendees number: ");
                        num = InputValidator.NumberInput(Console.ReadLine());
                        meetings = _service.GetMeetingsByAtendeesNumber(num);

                        break;
                    default:
                        break;
                }
                OutputMeetings(meetings);
                Console.Write("\nPress <Enter> to continue... ");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }
        }
        void OutputMeetings(List<Meeting> m)
        {
            Utils.WriteLineColor($"\n Found {m.Count}\n", ConsoleColor.Green);
            m.ForEach(x => Console.WriteLine(x + "\n  - - - - - - - -\n"));
        }
    }
}
