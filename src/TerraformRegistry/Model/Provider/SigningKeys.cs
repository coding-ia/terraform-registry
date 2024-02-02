using System.Text.Json.Serialization;

namespace TerraformRegistry.Model.Provider
{
    public class SigningKeys
    {
        [JsonPropertyName("gpg_public_keys")]
        public List<GPGPublicKeys> GPGPublicKeys { get; set; } = [];
    }
}
