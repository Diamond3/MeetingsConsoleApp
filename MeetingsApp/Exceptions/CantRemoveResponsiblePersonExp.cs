using System.Runtime.Serialization;

namespace MeetingsApp.Exceptions
{
    [Serializable]
    public class CantRemoveResponsiblePersonExp : Exception
    {
        public CantRemoveResponsiblePersonExp()
        {
        }

        public CantRemoveResponsiblePersonExp(string? message) : base(message)
        {
        }

        public CantRemoveResponsiblePersonExp(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CantRemoveResponsiblePersonExp(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}