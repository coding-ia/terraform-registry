using System.Text.Json.Serialization;

namespace TerraformRegistry.Model.Provider
{
    public class GPGPublicKeys
    {
        [JsonPropertyName("key_id")]
        public string KeyId { get; set; } = string.Empty;
        [JsonPropertyName("ascii_armor")]
        public string AsciiArmor { get; set; } = string.Empty;
        [JsonPropertyName("trust_signature")]
        public string TrustSignature { get; set; } = string.Empty;
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
        [JsonPropertyName("source_url")]
        public string SourceUrl { get; set; } = string.Empty;
    }
}
