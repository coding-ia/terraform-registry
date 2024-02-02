using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TerraformRegistry.Service;

namespace TerraformRegistry.Controllers
{
    [ApiController]
    [Route("/terraform/modules/v1/")]
    public class TerraformModuleController(IServiceConfiguration config, ILogger<TerraformModuleController> logger) : ControllerBase
    {
        private readonly IServiceConfiguration _config = config;
        private readonly ILogger<TerraformModuleController> _logger = logger;

        [HttpGet("{ns}/{name}/{system}/{version}/download")]
        public async Task<IActionResult> ModuleSource(string ns, string name, string system, string version)
        {
            _logger.LogInformation($"{ns}/{name}/{system}/{version}/download");

            var tms = new TerraformModuleService(_config.TERRAFORM_PROVIDER_BUCKET, _config.TERRAFORM_PROVIDER_BUCKET_REGION);
            string response = await tms.ModuleSource(ns, name, system, version);

            if (string.IsNullOrEmpty(response))
                return NotFound();

            return Ok(response);
        }
    }
}
