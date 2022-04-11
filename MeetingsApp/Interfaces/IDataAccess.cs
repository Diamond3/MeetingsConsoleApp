using System.Collections;

namespace MeetingsApp.Interfaces
{
    public interface IDataAccess
    {
        public void SaveToJson<T>(T objects) where T : IList;

        public T? LoadFromJson<T>() where T : IList;
    }
}
