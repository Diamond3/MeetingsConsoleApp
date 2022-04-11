using System.Runtime.Serialization;

namespace MeetingsApp.Exceptions
{
    [Serializable]
    public class MeetingDoesntExistExp : Exception
    {
        public MeetingDoesntExistExp()
        {
        }

        public MeetingDoesntExistExp(string? message) : base(message)
        {
        }

        public MeetingDoesntExistExp(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MeetingDoesntExistExp(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}