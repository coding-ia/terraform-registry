using System.Text.Json.Serialization;

namespace TerraformRegistry.Model.Provider
{
    public class TerraformProviderPlatform
    {
        [JsonPropertyName("os")]
        public string OS { get; set; } = string.Empty;
        [JsonPropertyName("arch")]
        public string Arch { get; set; } = string.Empty;
        [JsonPropertyName("filename")]
        public string Filename { get; set; } = string.Empty;
        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; } = string.Empty;
        [JsonPropertyName("shasums_url")]
        public string ShasumsUrl { get; set; } = string.Empty;
        [JsonPropertyName("shasums_signature_url")]
        public string ShasumsSignatureUrl { get; set; } = string.Empty;
        [JsonPropertyName("shasum")]
        public string Shasum { get; set; } = string.Empty;
        [JsonPropertyName("signing_keys")]
        public SigningKeys SigningKeys { get; set; } = new();
    }
}
