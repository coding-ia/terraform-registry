using System.Text.Json.Serialization;

namespace TerraformRegistry.Model.Module
{
    public class TerraformModule
    {
        [JsonPropertyName("versions")]
        public List<TerraformModuleVersion> Versions { get; set; } = [];
    }
}
