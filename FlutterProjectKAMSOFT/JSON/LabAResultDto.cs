using System.Text.Json.Serialization;

namespace FlutterProjectKAMSOFT.JSON
{
    public record LabAResultDto
    {
        [JsonPropertyName("test_name")]
        public string? TestName { get; init; }

        [JsonPropertyName("result")]
        public decimal Result { get; init; }

        [JsonPropertyName("scale")]
        public string? Unit { get; init; }

        [JsonPropertyName("test_code")]
        public string? TestCode { get; init; } = null;
    }
}
