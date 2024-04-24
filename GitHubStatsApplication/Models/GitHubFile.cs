using Newtonsoft.Json;

namespace GitHubStatsApplication.Models
{
    public class GitHubFile
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
