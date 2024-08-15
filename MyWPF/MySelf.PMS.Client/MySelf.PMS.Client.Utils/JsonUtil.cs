
using System.Text.Json;

namespace MySelf.PMS.Client.Utils
{
    public class JsonUtil
    {
        public static string Serializer<T>(T obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return System.Text.Json.JsonSerializer.Serialize<T>(obj, options);
        }

        public static T Deserializer<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
