using System.Text.Json.Serialization;

namespace TerraformRegistry.Model
{
    public class ServiceDiscovery
    {
        [JsonPropertyName("providers.v1")]
        public string Providers { get; set; } = string.Empty;

        [JsonPropertyName("login.v1")]
        public LoginV1 Login { get; set; } = new();
    }
}
