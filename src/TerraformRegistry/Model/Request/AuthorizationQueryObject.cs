namespace TerraformRegistry.Model.Request
{
    public class AuthorizationQueryObject
    {
        public string ClientId { get; set; } = string.Empty;
        public string CodeChallenge { get; set; } = string.Empty;
        public string CodeChallengeMethod { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string ResponseType { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
