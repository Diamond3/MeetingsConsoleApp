using MeetingsApp.Models;
using MeetingsApp.Utilities;

namespace MeetingsApp.Interfaces
{
    internal interface IMeetingService
    {
        void InsertMeeting(Meeting meeting);

        void DeleteMeeting(Guid id);

        void AddPersonToMeeting(Guid id, Person person);

        void RemovePersonFromMeeting(Guid id, Person person);

        List<Meeting> GetMeetingsByDescription(string text);

        List<Meeting> GetMeetingsByResponsiblePerson(Person person);

        List<Meeting> GetMeetingsByCategory(Utils.Category category);

        List<Meeting> GetMeetingsByType(Utils.Type type);

        List<Meeting> GetMeetingsByDates(DateTime fromDate, DateTime? toDate);

        List<Meeting> GetMeetingsByAtendeesNumber(int number);

        List<Meeting> GetMeetings();
    }
}
