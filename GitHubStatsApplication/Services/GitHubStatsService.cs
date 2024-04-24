using GitHubStatsApplication.Interfaces;
using GitHubStatsApplication.Models;
using GitHubStatsApplication.Request;
using GitHubStatsApplication.Response;
using Newtonsoft.Json;

namespace GitHubStatsApplication.Services
{
    public class GitHubStatsService : IGitHubStatsService
    {
        private readonly HttpClient _httpClient;

        public GitHubStatsService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<LetterFrequencyResponse> GetLetterFrequency(LetterFrequencyRequest request)
        {
            var orderBy = request.OrderBy ?? GitHubStatsConstants.OrderBy;
            var orderDirection = GitHubStatsConstants.OrderByDesc;

            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var files = JsonConvert.DeserializeObject<List<GitHubFile>>(responseBody);

            var letterFrequency = new Dictionary<char, int>();

            foreach (var file in files)
            {
                // Logic to calculate letter frequency
                if (file.Name.EndsWith(GitHubStatsConstants.JsFileExtension) || file.Name.EndsWith(GitHubStatsConstants.TsFileExtension))
                {
                    var fileNameWithoutExtension = GetFileNameWithoutExtension(file.Name).ToLower();
                    foreach (char c in fileNameWithoutExtension)
                    {
                        if (char.IsLetter(c))
                        {
                            if (!letterFrequency.ContainsKey(c))
                            {
                                letterFrequency[c] = GitHubStatsConstants.Zero;
                            }
                            letterFrequency[c]++;
                        }
                    }
                }
            }

            // Logic to order dictionary by key or value
            var sortedLetterFrequency = OrderDictionary(letterFrequency, orderBy, orderDirection);

            return new LetterFrequencyResponse
            {
                LetterFrequency = sortedLetterFrequency.ToDictionary(kv => kv.Key, kv => kv.Value),
                OrderBy = orderBy,
                OrderDirection = orderDirection
            };
        }

        private string GetFileNameWithoutExtension(string fileName)
        {
            var indexOfExtension = fileName.LastIndexOf('.');
            return indexOfExtension == -1 ? fileName : fileName.Substring(GitHubStatsConstants.Zero, indexOfExtension);
        }

        public IOrderedEnumerable<KeyValuePair<char, int>> OrderDictionary(Dictionary<char, int> dictionary, string orderBy, string orderDirection)
        {
            switch (orderBy.ToLower())
            {
                case "key":
                    return orderDirection.ToLower() == GitHubStatsConstants.OrderByDesc ? dictionary.OrderByDescending(pair => pair.Key) : dictionary.OrderBy(pair => pair.Key);
                case "value":
                default:
                    return orderDirection.ToLower() == GitHubStatsConstants.OrderByDesc ? dictionary.OrderByDescending(pair => pair.Value) : dictionary.OrderBy(pair => pair.Value);
            }
        }
    }
}
