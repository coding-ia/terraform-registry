using System.Text.Json.Serialization;

namespace TerraformRegistry.Model.Module
{
    public class TerraformModuleVersion
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;
    }
}
