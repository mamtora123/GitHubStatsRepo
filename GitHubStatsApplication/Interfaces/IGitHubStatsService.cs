using GitHubStatsApplication.Request;
using GitHubStatsApplication.Response;

namespace GitHubStatsApplication.Interfaces
{
    public interface IGitHubStatsService
    {
        Task<LetterFrequencyResponse> GetLetterFrequency(LetterFrequencyRequest request);

        IOrderedEnumerable<KeyValuePair<char, int>> OrderDictionary(Dictionary<char, int> dictionary, string orderBy, string orderDirection);
    }
}
