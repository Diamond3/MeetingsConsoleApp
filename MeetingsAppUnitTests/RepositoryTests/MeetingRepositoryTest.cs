using MeetingsApp.Exceptions;
using MeetingsApp.Interfaces;
using MeetingsApp.Models;
using MeetingsApp.Repositories;
using MeetingsApp.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MeetingsAppUnitTests.RepositoryTests
{
    public class MeetingRepositoryTest
    {
        [Fact]
        void AddPersonToMeetingTest_AlreadyExist_ShouldThrowMemeberAlreadyExistsExp()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "Description", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Petras meeting", new Person("Petras", DateTime.Now), "Description2", Utils.Category.Hub, Utils.Type.InPerson, DateTime.Now, DateTime.Now),
            };


            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);

            var repository = new MeetingRepository(dataAccess.Object);

            // Act
            Assert.Throws<MemberAlreadyExistsExp>(() => repository.AddPersonToMeeting(meetings[0].Id, new Person("Jonas")));
        }

        [Fact]
        void GetMeetingsByCategoryTest()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "Description", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Petras meeting", new Person("Petras", DateTime.Now), "Description2", Utils.Category.Hub, Utils.Type.InPerson, DateTime.Now, DateTime.Now),
            };
            var expected = new List<Meeting>() { meetings[1] };

            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);

            var repository = new MeetingRepository(dataAccess.Object);

            // Act
            var actual = (List<Meeting>)repository.GetMeetingsByCategory(Utils.Category.Hub);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        void GetMeetingsByDescriptionTest()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "short .des", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Petras meeting", new Person("Petras", DateTime.Now), "something .description", Utils.Category.Hub, Utils.Type.InPerson, DateTime.Now, DateTime.Now),
                new Meeting("Arturas meeting", new Person("Arturas", DateTime.Now), "empty text here", Utils.Category.TeamBuilding, Utils.Type.Live, DateTime.Now, DateTime.Now)
            };
            var expected = new List<Meeting>() { meetings[0], meetings[1] };

            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);

            var repository = new MeetingRepository(dataAccess.Object);

            // Act
            var actual = (List<Meeting>)repository.GetMeetingsByDescription(".des");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        void GetMeetingsByResponsiblePersonTest_NotExistingPerson_ReturnsZeroEntries()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "Description", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Petras meeting", new Person("Petras", DateTime.Now), "Description2", Utils.Category.Hub, Utils.Type.InPerson, DateTime.Now, DateTime.Now),
            };
            int expected = 0;

            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);

            var repository = new MeetingRepository(dataAccess.Object);

            // Act
            var actual = (List<Meeting>)repository.GetMeetingsByResponsiblePerson(new Person("OtherPetras"));

            // Assert
            Assert.Equal(expected, actual.Count);
        }

        [Fact]
        void GetMeetingsByResponsiblePersonTest_ReturnsTwoEntries()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "Description", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Petras meeting", new Person("Petras", DateTime.Now), "Description2", Utils.Category.Hub, Utils.Type.InPerson, DateTime.Now, DateTime.Now),
                new Meeting("Arturas meeting", new Person("Arturas", DateTime.Now), "Description desc", Utils.Category.TeamBuilding, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Other meeting", new Person("Petras", DateTime.Now), "something", Utils.Category.CodeMonkey, Utils.Type.Live, DateTime.Now, DateTime.Now)
            };
            var expected = new List<Meeting>() { meetings[1], meetings[3] };


            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);

            var repository = new MeetingRepository(dataAccess.Object);

            // Act
            var actual = (List<Meeting>)repository.GetMeetingsByResponsiblePerson(new Person("Petras"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        void RemovePersonFromMeetingTest_NonExistingMeeting_ThrowsMeetingDoesntExistExp()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "Description", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Petras meeting", new Person("Petras", DateTime.Now), "Description2", Utils.Category.Hub, Utils.Type.InPerson, DateTime.Now, DateTime.Now),
            };
            var nonExistingId = Guid.NewGuid();

            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);

            var repository = new MeetingRepository(dataAccess.Object);

            // Assert
            Assert.Throws<MeetingDoesntExistExp>(() => repository.RemovePersonFromMeeting(nonExistingId, new Person("Jonas")));
        }

        [Fact]
        void RemovePersonFromMeetingTest_RemovingResponsiblePerson_ThrowsCantRemoveResponsiblePersonExp()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "Description", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
            };

            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);

            var repository = new MeetingRepository(dataAccess.Object);

            // Assert
            Assert.Throws<CantRemoveResponsiblePersonExp>(() => repository.RemovePersonFromMeeting(meetings[0].Id, new Person("Jonas")));
        }

        [Fact]
        void RemovePersonFromMeetingTest_RemovesSuccessfully()
        {
            // Arrange
            var meetings = new List<Meeting>()
            {
                new Meeting("Jonas meeting", new Person("Jonas", DateTime.Now), "Description", Utils.Category.Short, Utils.Type.Live, DateTime.Now, DateTime.Now),
                new Meeting("Petras meeting", new Person("Petras", DateTime.Now), "Description2", Utils.Category.Hub, Utils.Type.InPerson, DateTime.Now, DateTime.Now),
            };
            meetings[0].Attendees.Add(new Person("KitasJonas")); //adding new memeber to the meeting;

            var dataAccess = new Mock<IDataAccess>();
            dataAccess.Setup(x => x.LoadFromJson<List<Meeting>>()).Returns(meetings);
            dataAccess.Setup(x => x.SaveToJson(meetings));

            var repository = new MeetingRepository(dataAccess.Object);

            // Act
            repository.RemovePersonFromMeeting(meetings[0].Id, new Person("KitasJonas"));

            // Assert
            dataAccess.Verify(v => v.SaveToJson(meetings), Times.Once());
        }
    }
}
