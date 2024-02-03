using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using TerraformRegistry.Service;

namespace TerraformRegistry.Controllers
{
    [ApiController]
    [Route("/terraform/modules/v1/")]
    public class TerraformModuleController(IServiceConfiguration config, ILogger<TerraformModuleController> logger) : ControllerBase
    {
        private readonly IServiceConfiguration _config = config;
        private readonly ILogger<TerraformModuleController> _logger = logger;

        [HttpGet("{ns}/{name}/{system}/versions")]
        public async Task<IActionResult> ModuleVersion(string ns, string name, string system)
        {
            _logger.LogInformation($"{ns}/{name}/{system}/versions");

            var tms = new TerraformModuleService(_config.TERRAFORM_PROVIDER_BUCKET, _config.TERRAFORM_PROVIDER_BUCKET_REGION);
            string response = await tms.ModuleVersion(ns, name, system);

            if (string.IsNullOrEmpty(response))
                return NotFound();

            JsonDocument doc = JsonDocument.Parse(response);

            return Ok(doc);
        }

        [HttpGet("{ns}/{name}/{system}/{version}/download")]
        public IActionResult ModuleDownload(string ns, string name, string system, string version)
        {
            _logger.LogInformation($"{ns}/{name}/{system}/{version}/download");

            var tms = new TerraformModuleService(_config.TERRAFORM_PROVIDER_BUCKET, _config.TERRAFORM_PROVIDER_BUCKET_REGION);
            string downloadUrl = tms.ModuleDownload(ns, name, system, version);

            if (string.IsNullOrEmpty(downloadUrl))
                return NotFound();

            Response.Headers.Add(new KeyValuePair<string, StringValues>("X-Terraform-Get", downloadUrl));

            return new NoContentResult();
        }
    }
}
