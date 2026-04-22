using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlutterProjectKAMSOFT.JSON
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public static string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, Options);
        }

        public static T? Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(json, Options);
            }
            catch
            {
                return default;
            }
        }

        public static T? Deserialize<T>(object rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput.ToString()))
                return default;

            if (rawInput is string str)
                return Deserialize<T>(str);


            var jsonString = JsonSerializer.Serialize(rawInput, Options);
            return Deserialize<T>(jsonString);
        }
    }
}
