namespace TerraformRegistry.Model.Request
{
    public class AccessTokenFormObject
    {
        public string ClientId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string CodeVerifier { get; set; } = string.Empty;
        public string GrantType { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
    }
}
