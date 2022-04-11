using System.Runtime.Serialization;

namespace MeetingsApp.Exceptions
{
    [Serializable]
    public class MemberAlreadyExistsExp : Exception
    {
        public MemberAlreadyExistsExp()
        {
        }

        public MemberAlreadyExistsExp(string? message) : base(message)
        {
        }

        public MemberAlreadyExistsExp(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MemberAlreadyExistsExp(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}