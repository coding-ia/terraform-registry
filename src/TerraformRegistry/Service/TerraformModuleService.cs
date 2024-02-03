using Amazon.S3;
using Amazon.S3.Model;
using Octokit;

namespace TerraformRegistry.Service
{
    internal class TerraformModuleService(string bucketName, string region)
    {
        private readonly string _bucketName = bucketName;
        private readonly Amazon.RegionEndpoint _region = Amazon.RegionEndpoint.GetBySystemName(region);

        internal async Task<string> ModuleVersion(string name_space, string name, string system)
        {
            var data = await Content(_bucketName, $"modules/{name_space}/{name}/{system}.json");
            return data;
        }

        internal string ModuleDownload(string name_space, string name, string system, string version)
        {
            string repo = $"terraform-{system}-{name}";
            var downloadUrl = GetReleaseTagDownloadUrl(name_space, repo, version).GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(downloadUrl))
                downloadUrl = $"{downloadUrl}?archive=tar.gz";

            return downloadUrl;
        }

        private async Task<string> GetReleaseTagDownloadUrl(string owner, string repo, string version, Uri? baseAddress = null)
        {
            GitHubClient client;

            if (baseAddress == null)
            {
                client = new GitHubClient(new ProductHeaderValue("terraform-registry"));
            }
            else
            {
                client = new GitHubClient(new ProductHeaderValue("terraform-registry"), baseAddress);
            }

            var releases = await client.Repository.Release.GetAll(owner, repo);
            var release = releases.FirstOrDefault<Release>(x => x.TagName.Contains(version, StringComparison.OrdinalIgnoreCase));

            return release?.TarballUrl ?? "";
        }

        private async Task<string> Content(string? bucketName, string key)
        {
            string content = string.Empty;
            AmazonS3Client client = new(_region);

            GetObjectRequest request = new()
            {
                BucketName = bucketName,
                Key = key
            };

            using GetObjectResponse response = await client.GetObjectAsync(request);
            using Stream responseStream = response.ResponseStream;
            using StreamReader reader = new(responseStream);
            content = reader.ReadToEnd();

            return content;
        }
    }
}
