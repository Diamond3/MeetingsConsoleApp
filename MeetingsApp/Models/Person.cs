using Newtonsoft.Json;

namespace MeetingsApp.Models
{
    public class Person
    {
        [JsonProperty]
        public string FullName { get; private set; }

        [JsonProperty]
        public DateTime TimeAdded { get; set; }

        public Person()
        {

        }

        public Person(string fullName)
        {
            FullName = fullName;
        }

        public Person(string fullName, DateTime time)
        {
            FullName = fullName;
            TimeAdded = time;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return this.FullName == ((Person)obj).FullName;
        }
    }
}