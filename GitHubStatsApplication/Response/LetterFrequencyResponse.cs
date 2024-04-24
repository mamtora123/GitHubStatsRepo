namespace GitHubStatsApplication.Response
{
    public class LetterFrequencyResponse
    {
        public Dictionary<char, int> LetterFrequency { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
    }
}
