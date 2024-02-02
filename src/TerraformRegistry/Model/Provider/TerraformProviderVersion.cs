using System.Text.Json.Serialization;

namespace TerraformRegistry.Model.Provider
{
    public class TerraformProviderVersion
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
        [JsonPropertyName("protocols")]
        public List<string> Protocols { get; set; } = [];
        [JsonPropertyName("platforms")]
        public List<TerraformProviderPlatform> Platforms { get; set; } = [];
    }
}
