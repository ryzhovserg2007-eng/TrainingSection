using Newtonsoft.Json;
using System.IO;

namespace Model.Data
{
    public class JsonSerializer : Serializer
    {
        public override void SaveToFile(string filePath, object data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public override T LoadFromFile<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public override string GetFileExtension() => ".json";

        public override string SerializeToString(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        public override T DeserializeFromString<T>(string data) where T : class
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
