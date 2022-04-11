using MeetingsApp.Controlers;
using MeetingsApp.DataControl;
using MeetingsApp.Interfaces;
using MeetingsApp.Models;
using MeetingsApp.Repositories;
using MeetingsApp.Services;
using MeetingsApp.Utilities;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services
    .AddScoped<IDataAccess, DataAccess>()
    .AddScoped<IMeetingService, MeetingService>()
    .AddScoped<IMeetingRepository, MeetingRepository>()
    .BuildServiceProvider();

Console.Title = "MeetingsApp";

string options = "0 - Create a new meeting\n" +
                "1 - My meetings\n" + 
                "2 - Meeting search\n" +
                "3 - Exit the application\n" +
                "Choose the operation: ";

Console.Write("Login using your full name: ");
string userName = InputValidator.StringInput(Console.ReadLine());
Person currentUser = new Person(userName);

IView myMeetingsView = new MyMeetingsView(services.BuildServiceProvider().GetRequiredService<IMeetingService>(), currentUser);
IView searchMeetingsView = new SearchMeetingsView(services.BuildServiceProvider().GetRequiredService<IMeetingService>());
IView newMeetingView = new NewMeetingView(services.BuildServiceProvider().GetRequiredService<IMeetingService>(), currentUser);

while (true)
{
    Console.Clear();
    Console.Write(options);
    int choice = InputValidator.NumberInput(Console.ReadLine(), 3);

    switch (choice)
    {
        case 0:
            Console.Clear();
            newMeetingView.LoadBody();

            break;
        case 1:
            Console.Clear();
            myMeetingsView.LoadBody();
        
            break;
        case 2:
            Console.Clear();
            searchMeetingsView.LoadBody();

            break;
        default:
            return;
    }
}






