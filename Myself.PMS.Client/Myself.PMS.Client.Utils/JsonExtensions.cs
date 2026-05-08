
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Myself.PMS.Client.Utils
{
    public static class JsonExtensions
    {
        // 缓存选项实例，避免重复创建
        private static readonly JsonSerializerOptions DefaultOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // 序列化用小驼峰
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull  // 忽略 null
        };

        public static string Serialize<T>(this T obj, JsonSerializerOptions options = null)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, options ?? DefaultOptions);
        }

        public static T Deserialize<T>(this string json, JsonSerializerOptions options = null)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, options ?? DefaultOptions);
        }
    }

}
