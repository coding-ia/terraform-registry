using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TerraformRegistry.Controllers
{
    [ApiController]
    [Route("/")]
    public class TerraformServiceController(ILogger<TerraformServiceController> logger, IServiceConfiguration config) : ControllerBase
    {
        private readonly ILogger<TerraformServiceController> _logger = logger;
        private readonly IServiceConfiguration _config = config;

        [HttpGet(".well-known/terraform.json")]
        public IActionResult ServiceDiscovery()
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
                return NoContent();
            }

            JsonDocument doc = JsonDocument.Parse(discovery);

            return Ok(doc);
        }
    }
}
