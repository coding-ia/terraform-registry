using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TerraformRegistry.Controllers
{
    [ApiController]
    [Route("/terraform/providers/v1/")]
    internal class TerraformProviderController(IServiceConfiguration config, ILogger<TerraformProviderController> logger) : ControllerBase
    {
        private readonly IServiceConfiguration _config = config;
        private readonly ILogger<TerraformProviderController> _logger = logger;

        [HttpGet("{ns}/{name}/{version}/download/{os}/{arch}")]
        public async Task<IActionResult> ProviderPackage(string ns, string name, string version, string os, string arch)
        {
            if (_config.AUTHENTICATION_ENABLED)
            {
                try
                {
                    var token = TerraformAuthenticationService.ValidateJWTToken(ControllerContext.HttpContext.Request.Headers.Authorization, _config.TOKEN_SECRET_KEY);

                    if (token == null)
                        return Unauthorized();
                }
                catch (Exception)
                {
                    return Unauthorized();
                }
            }

            _logger.LogInformation($"{ns}/{name}/{version}/download/{os}/{arch}");

            var tps = new TerraformProviderService(_config.TERRAFORM_PROVIDER_BUCKET, _config.TERRAFORM_PROVIDER_BUCKET_REGION);

            string response = await tps.ProviderPackage(ns, name, version, os, arch);

            if (string.IsNullOrEmpty(response))
                return NotFound();

            return Ok(response);
        }
    }
}
