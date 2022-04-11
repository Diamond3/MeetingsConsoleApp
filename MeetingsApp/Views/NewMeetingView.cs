using MeetingsApp.Interfaces;
using MeetingsApp.Models;
using MeetingsApp.Utilities;

namespace MeetingsApp.Controlers
{
    internal class NewMeetingView: IView
    {
        private readonly IMeetingService _service;

        private readonly Person _currentUser;

        public NewMeetingView(IMeetingService service, Person person)
        {
            _service = service;
            _currentUser = person;
        }

        private void ShowTitle()
        {
            Console.WriteLine("Creating new meeting\n");
        }

        public void LoadBody()
        {
            Console.Clear();
            ShowTitle();
            Console.Write("Name: ");
            var meetingName = InputValidator.StringInput(Console.ReadLine());

            Console.Write("Description: ");
            var description = InputValidator.StringInput(Console.ReadLine());

            Console.Write("Category (0 - CodeMonkey | 1 - Hub | 2 - Short | 3 - TeamBuilding): ");
            var category = InputValidator.NumberInput(Console.ReadLine(), (int)Utils.Category.TeamBuilding);

            Console.Write("Type (0 - Live | 1 - InPerson): ");
            var type = InputValidator.NumberInput(Console.ReadLine(), (int)Utils.Type.InPerson);

            Console.Write("Start date (ex. 2022-01-01): ");
            var startDate = InputValidator.DateInput(Console.ReadLine());

            Console.Write("End date (ex. 2022-01-01): ");
            var endDate = InputValidator.DateInput(Console.ReadLine());

            _currentUser.TimeAdded = DateTime.Now;
            var meeting = new Meeting(meetingName, _currentUser, description, (Utils.Category)category, (Utils.Type)type, startDate, endDate);

            try
            {
                _service.InsertMeeting(meeting);
                Utils.WriteLineColor("Saved successfully!", ConsoleColor.Green);
            }
            catch (Exception)
            {
                Utils.WriteLineColor("Failed to save the meeting!", ConsoleColor.Red);
            }
            Console.Write("Press <Enter> to go back... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
