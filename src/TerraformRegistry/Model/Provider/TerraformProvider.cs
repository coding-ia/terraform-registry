using System.Text.Json.Serialization;

namespace TerraformRegistry.Model.Provider
{
    public class TerraformProvider
    {
        [JsonPropertyName("versions")]
        public List<TerraformProviderVersion> Versions { get; set; } = [];
    }
}
