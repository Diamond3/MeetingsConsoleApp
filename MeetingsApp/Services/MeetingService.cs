using MeetingsApp.Interfaces;
using MeetingsApp.Models;
using MeetingsApp.Utilities;

namespace MeetingsApp.Services
{
    internal class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _repo;

        public MeetingService(IMeetingRepository meetingRepository) {
            _repo = meetingRepository;
        }

        public void AddPersonToMeeting(Guid id, Person person)
        {
            _repo.AddPersonToMeeting(id, person);
        }

        public void DeleteMeeting(Guid id)
        {
            _repo.DeleteMeeting(id);
        }

        public List<Meeting> GetMeetings()
        {
            return (List<Meeting>)_repo.GetMeetings();
        }

        public List<Meeting> GetMeetingsByAtendeesNumber(int number)
        {
            return (List<Meeting>)_repo.GetMeetingsByAtendeesNumber(number);
        }

        public List<Meeting> GetMeetingsByCategory(Utils.Category category)
        {
            return (List<Meeting>)_repo.GetMeetingsByCategory(category);
        }

        public List<Meeting> GetMeetingsByDates(DateTime fromDate, DateTime? toDate)
        {
            return (List<Meeting>)_repo.GetMeetingsByDates(fromDate, toDate);
        }

        public List<Meeting> GetMeetingsByDescription(string text)
        {
            return (List<Meeting>)_repo.GetMeetingsByDescription(text);
        }

        public List<Meeting> GetMeetingsByResponsiblePerson(Person person)
        {
            return (List<Meeting>)_repo.GetMeetingsByResponsiblePerson(person);
        }

        public List<Meeting> GetMeetingsByType(Utils.Type type)
        {
            return (List<Meeting>)_repo.GetMeetingsByType(type);
        }

        public void InsertMeeting(Meeting meeting)
        {
            _repo.InsertMeeting(meeting);
        }

        public void RemovePersonFromMeeting(Guid id, Person person)
        {
            _repo.RemovePersonFromMeeting(id, person);
        }
    }
}
