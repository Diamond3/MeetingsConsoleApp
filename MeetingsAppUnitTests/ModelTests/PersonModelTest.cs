using MeetingsApp.Models;
using Xunit;

namespace MeetingsAppUnitTests.ModelTests
{
    public class PersonModelTest
    {
        [Fact]
        public void EqualMethod_SameNameDifferentDate_Equal()
        {
            var person1 = new Person("Tomas", new System.DateTime(2000, 10, 20));
            var person2 = new Person("Tomas", new System.DateTime(2022, 10, 20));

            Assert.Equal(person1, person2);
        }

        [Fact]
        public void EqualMethod_CaseSesitive_NotEqual()
        {
            var person1 = new Person("tomas", new System.DateTime(2000, 10, 20));
            var person2 = new Person("Tomas", new System.DateTime(2022, 10, 20));

            Assert.NotEqual(person1, person2);
        }
    }
}