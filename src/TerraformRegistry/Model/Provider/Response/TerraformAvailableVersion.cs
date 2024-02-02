namespace TerraformRegistry.Model.Provider.Response
{
    public class TerraformAvailableVersion
    {
        public string Version { get; set; } = string.Empty;
        public List<string> Protocols { get; set; } = [];
        public List<TerraformAvailablePlatform> Platforms { get; set; } = [];
    }
}
