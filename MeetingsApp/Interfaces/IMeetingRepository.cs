using MeetingsApp.Models;
using MeetingsApp.Utilities;

namespace MeetingsApp.Interfaces
{
    public interface IMeetingRepository
    {
        void InsertMeeting(Meeting meeting);

        void DeleteMeeting(Guid id);

        void AddPersonToMeeting(Guid id, Person person);

        void RemovePersonFromMeeting(Guid id, Person person);

        IEnumerable<Meeting> GetMeetingsByDescription(string text);

        IEnumerable<Meeting> GetMeetingsByResponsiblePerson(Person person);

        IEnumerable<Meeting> GetMeetingsByCategory(Utils.Category category);

        IEnumerable<Meeting> GetMeetingsByType(Utils.Type type);

        IEnumerable<Meeting> GetMeetingsByDates(DateTime fromDate, DateTime? toDate);

        IEnumerable<Meeting> GetMeetingsByAtendeesNumber(int number);

        IEnumerable<Meeting> GetMeetings();
    }
}
