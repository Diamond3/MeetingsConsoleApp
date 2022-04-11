using MeetingsApp.Interfaces;
using Newtonsoft.Json;
using System.Collections;

namespace MeetingsApp.DataControl
{
    public class DataAccess: IDataAccess
    {
        public void SaveToJson<T>(T objects) where T : IList
        {
            string fileName = ".\\MeetingsData.json";
            string json = JsonConvert.SerializeObject(objects, Formatting.Indented);
            System.IO.File.WriteAllText(fileName, json);
        }

        public  T? LoadFromJson<T>() where T : IList
        {
            string fileName = ".\\MeetingsData.json";
            return JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(fileName));
        }
    }
}
