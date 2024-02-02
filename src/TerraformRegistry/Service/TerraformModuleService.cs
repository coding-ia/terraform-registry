using Amazon.S3;
using Amazon.S3.Model;
using Octokit;
using System.Text.Json;
using TerraformRegistry.Model.Module;

namespace TerraformRegistry.Service
{
    internal class TerraformModuleService(string bucketName, string region)
    {
        private readonly string _bucketName = bucketName;
        private readonly Amazon.RegionEndpoint _region = Amazon.RegionEndpoint.GetBySystemName(region);

        internal async Task<string> ModuleSource(string name_space, string name, string system, string version)
        {
            string data = await Content(_bucketName, $"modules/{name_space}/{system}.json");
            if (string.IsNullOrEmpty(data)) return string.Empty;

            var tm = JsonSerializer.Deserialize<TerraformModule>(data);

            if (tm == null) return string.Empty;
            var moduleVersion = tm.Versions.Find(x => x.Version == version);

            if (moduleVersion == null) return string.Empty;

            string repo = $"terraform-{system}-{name}";
            string downloadUrl = await GetReleaseTagDownload(name_space, repo, moduleVersion.Version);

            return downloadUrl;
        }

        private async Task<string> GetReleaseTagDownload(string owner, string repo, string tagName, Uri? baseAddress = null)
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

            var tags = await client.Repository.GetAllTags(owner, repo);
            var tag = tags.First<RepositoryTag>(x => x.Name == tagName);
            return tag.TarballUrl ?? "";
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
