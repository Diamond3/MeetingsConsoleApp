using MeetingsApp.Utilities;
using Newtonsoft.Json;

namespace MeetingsApp.Models
{
    public class Meeting
    {
        public Guid Id { get; }

        public string Name { get; }

        public Person ResponsiblePerson { get; }

        public string Description { get; }

        public Utils.Category Category { get; }

        public Utils.Type Type { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public List<Person> Attendees { get; set; }

        public Meeting(string name, Person responsiblePerson, string description,
            Utils.Category category, Utils.Type type, DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Attendees = new List<Person>() { ResponsiblePerson }; //resposible person is added as attendee automaticaly
        }

        [JsonConstructor]
        public Meeting(Guid id, string name, Person responsiblePerson, string description,
            Utils.Category category, Utils.Type type, DateTime startDate, DateTime endDate, List<Person> attendees)
        {
            Id = id;
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Attendees = attendees;
        }

        public Meeting()
        {

        }

        public override string ToString()
        {
            return ($"Meeting: {Name} | {ResponsiblePerson.FullName}\n  Category: {Category} | Type: {Type}\n" +
                $"  Date: {StartDate} - {EndDate}\n" +
                $"  Description: {Description}\n" + 
                $"  Attendees ({Attendees.Count}):\n " +
                $" ({string.Join(",\n  ", Attendees.Select(x => x.FullName + " " + x.TimeAdded))})"
                );
        }
    }
}
