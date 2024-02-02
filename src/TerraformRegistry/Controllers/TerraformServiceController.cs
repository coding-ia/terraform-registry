using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace TerraformRegistry.Controllers
{
    [ApiController]
    [Route("/")]
    public class TerraformServiceController(ILogger<TerraformServiceController> logger, IServiceConfiguration config) : ControllerBase
    {
        private readonly ILogger<TerraformServiceController> _logger = logger;
        private readonly IServiceConfiguration _config = config;

        [HttpGet(".well-known/terraform.json")]
        public HttpResponseMessage ServiceDiscovery()
        {
            string discovery = "{\"providers.v1\": \"/terraform/providers/v1/\"}";

            _logger.LogInformation(".well-known/terraform.json");

            var assembly = typeof(TerraformServiceController).Assembly;
            Stream? resource = assembly?.GetManifestResourceStream("TerraformRegistry.serviceDiscovery.json");

            if (resource != null)
            {
                using StreamReader reader = new(resource);
                discovery = reader.ReadToEnd();
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(discovery),
            };
        }
    }
}
