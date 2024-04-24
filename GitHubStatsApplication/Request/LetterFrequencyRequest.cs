using System.ComponentModel.DataAnnotations;

namespace GitHubStatsApplication.Request
{
    public class LetterFrequencyRequest
    {
        [EnumDataType(typeof(OrderByOptions))]
        public string OrderBy { get; set; }
    }

    public enum OrderByOptions
    {
        Key,
        Value
    }

}
