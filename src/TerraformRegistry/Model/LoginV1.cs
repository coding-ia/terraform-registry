using System.Text.Json.Serialization;

namespace TerraformRegistry.Model
{
    public class LoginV1
    {
        [JsonPropertyName("client")]
        public string Client { get; set; } = string.Empty;
        [JsonPropertyName("grant_types")]
        public List<string> GrantTypes { get; set; } = [];
        [JsonPropertyName("authz")]
        public string Authz { get; set; } = string.Empty;
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}
