using System.IO;
using System.Xml.Serialization;

namespace Model.Data
{
    public class XmlSerializer : Serializer
    {
        public override void SaveToFile(string filePath, object data)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(data.GetType());
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public override T LoadFromFile<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath)) return null;

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filePath))
            {
                return serializer.Deserialize(reader) as T;
            }
        }

        public override string GetFileExtension() => ".xml";

        public override string SerializeToString(object data)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(data.GetType());
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, data);
                return writer.ToString();
            }
        }

        public override T DeserializeFromString<T>(string data) where T : class
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var reader = new StringReader(data))
            {
                return serializer.Deserialize(reader) as T;
            }
        }
    }
}
