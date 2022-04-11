using MeetingsApp.Exceptions;
using MeetingsApp.Interfaces;
using MeetingsApp.Models;
using MeetingsApp.Utilities;

namespace MeetingsApp.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly IDataAccess _dataAccess;

        public MeetingRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void AddPersonToMeeting(Guid id, Person person)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) throw new MeetingDoesntExistExp();

            var meeting = meetings.SingleOrDefault(m => m.Id == id);
            if (meeting == null) throw new MeetingDoesntExistExp();

            if (meeting.Attendees.Contains(person)) throw new MemberAlreadyExistsExp();
            meeting.Attendees.Add(person);

            _dataAccess.SaveToJson(meetings);
        }

        public void DeleteMeeting(Guid id)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) throw new MeetingDoesntExistExp();

            var meeting = meetings.SingleOrDefault(m => m.Id == id);
            if (meeting == null) throw new MeetingDoesntExistExp();

            meetings.Remove(meeting);
            _dataAccess.SaveToJson(meetings);
        }

        public IEnumerable<Meeting> GetMeetings()
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) return new List<Meeting>();
            return meetings;
        }

        public IEnumerable<Meeting> GetMeetingsByAtendeesNumber(int number)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) return new List<Meeting>();

            return meetings.Where(m => m.Attendees.Count >= number).ToList();
        }

        public IEnumerable<Meeting> GetMeetingsByCategory(Utils.Category category)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) return new List<Meeting>();

            return meetings.Where(m => m.Category == category).ToList();
        }

        public IEnumerable<Meeting> GetMeetingsByDates(DateTime fromDate, DateTime? toDate)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) return new List<Meeting>();

            if (toDate == null) return meetings.Where(m => m.StartDate >= fromDate).ToList();

            return meetings.Where(m => m.StartDate >= fromDate && m.EndDate <= toDate).ToList();
        }

        public IEnumerable<Meeting> GetMeetingsByDescription(string text)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) return new List<Meeting>();

            return meetings.Where(m => m.Description.ToLower().Contains(text.ToLower())).ToList();
        }

        public IEnumerable<Meeting> GetMeetingsByResponsiblePerson(Person person)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) return new List<Meeting>();

            return meetings.Where(m => m.ResponsiblePerson.Equals(person)).ToList();
        }

        public IEnumerable<Meeting> GetMeetingsByType(Utils.Type type)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) return new List<Meeting>();

            return meetings.Where(m => m.Type == type).ToList();
        }

        public void InsertMeeting(Meeting meeting)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null)
            {
                meetings = new List<Meeting>();
            }
            meetings.Add(meeting);
            _dataAccess.SaveToJson(meetings);
        }

        public void RemovePersonFromMeeting(Guid id, Person person)
        {
            var meetings = _dataAccess.LoadFromJson<List<Meeting>>();
            if (meetings == null) throw new MeetingDoesntExistExp();

            var meeting = meetings.SingleOrDefault(m => m.Id == id);
            if (meeting == null) throw new MeetingDoesntExistExp();

            if (meeting.ResponsiblePerson.Equals(person)) throw new CantRemoveResponsiblePersonExp();
            meeting.Attendees.Remove(person);

            _dataAccess.SaveToJson(meetings);
        }
    }
}
