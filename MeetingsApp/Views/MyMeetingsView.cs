using MeetingsApp.Exceptions;
using MeetingsApp.Interfaces;
using MeetingsApp.Models;
using MeetingsApp.Utilities;

namespace MeetingsApp.Controlers
{
    internal class MyMeetingsView : IView
    {
        private readonly IMeetingService _service;

        private readonly Person _currentUser;

        public MyMeetingsView(IMeetingService service, Person person)
        {
            _service = service;
            _currentUser = person;
        }

        public void ShowTitle()
        {
            Console.WriteLine("My meetings\n");
        }

        public void LoadBody()
        {
            while (true)
            {
                Console.Clear();
                ShowTitle();
                try
                {
                    List<Meeting> meetings = _service.GetMeetingsByResponsiblePerson(_currentUser);
                    Utils.WriteLineColor($"Found {meetings.Count}\n", ConsoleColor.Green);

                    Console.WriteLine("0 - Go back");
                    meetings.ForEach(m => Console.WriteLine($"{meetings.IndexOf(m) + 1} - {m}\n  - - - - - - - -\n"));

                    Console.Write("\nSelect meeting: ");
                    var indx = InputValidator.NumberInput(Console.ReadLine(), meetings.Count);
                    if (indx == 0) return;

                    Console.Clear();
                    Console.WriteLine(meetings[indx - 1].ToString() + "\n");

                    Console.WriteLine("0 - Go back");
                    Console.WriteLine("1 - Delete meeting");
                    Console.WriteLine("2 - Add person to the meeting");
                    Console.WriteLine("3 - Remove person from the meeting");
                    Console.Write("Choose operation: ");

                    var option = InputValidator.NumberInput(Console.ReadLine(), 3);

                    switch (option)
                    {
                        case 0: return;
                        case 1: //delete meeting
                            try
                            {
                                _service.DeleteMeeting(meetings[indx - 1].Id);
                                Utils.WriteLineColor("Meeting was deleted.", ConsoleColor.Green);
                            }
                            catch (MeetingDoesntExistExp)
                            {
                                Utils.WriteLineColor("Meeting doesnt exist!", ConsoleColor.Red);
                                goto case 1;
                            }
                            break;
                        case 2: //add person
                            Console.Write("Name of person to add: ");
                            var fullName = InputValidator.StringInput(Console.ReadLine());
                            try
                            {
                                _service.AddPersonToMeeting(meetings[indx - 1].Id, new Person(fullName, DateTime.Now));
                                Utils.WriteLineColor("Person was added.", ConsoleColor.Green);
                            }
                            catch(MemberAlreadyExistsExp)
                            {
                                Utils.WriteLineColor("User is already in meeting!", ConsoleColor.Red);
                                goto case 2;
                            }
                            break;
                        case 3: //remove person
                            Console.Write("Name of person to remove: ");
                            fullName = InputValidator.StringInput(Console.ReadLine());
                            try
                            {
                                _service.RemovePersonFromMeeting(meetings[indx - 1].Id, new Person(fullName));
                                Utils.WriteLineColor("Person was removed.", ConsoleColor.Green);
                            }
                            catch (CantRemoveResponsiblePersonExp)
                            {
                                Utils.WriteLineColor("User is reponsible person!", ConsoleColor.Red);
                                goto case 3;
                            }
                            break;
                    }
                }
                catch (Exception)
                {
                    Utils.WriteLineColor("Error occured!", ConsoleColor.Red);

                    Console.Write("Press <Enter> to continue... ");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                }
                WaitForEnter();
            }
        }
        private void WaitForEnter()
        {
            Console.Write("Press <Enter> to continue... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
