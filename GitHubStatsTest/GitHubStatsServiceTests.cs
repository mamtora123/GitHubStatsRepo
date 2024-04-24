using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubStatsApplication.Interfaces;
using GitHubStatsApplication.Request;
using GitHubStatsApplication.Services;
using Moq;
using Moq.Protected;
using Xunit;

namespace GitHubStatsTest
{
    public class GitHubStatsServiceTests
    {
        [Fact]
        public async Task GetLetterFrequency_ReturnsLetterFrequencyResponse()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.github.com/repos/lodash/lodash/contents/src");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubStats");

            var service = new GitHubStatsService(httpClient);
            var request = new LetterFrequencyRequest();

            // Act
            var response = await service.GetLetterFrequency(request);

            // Assert
            Assert.NotNull(response);
            // Add more assertions as needed
        }


        [Fact]
        public void OrderDictionary_OrdersByKey_Descending()
        {
            // Arrange
            var dictionary = new Dictionary<char, int> { { 'a', 3 }, { 'b', 2 }, { 'c', 1 } };
            var service = new GitHubStatsService(new HttpClient());

            // Act
            var result = service.OrderDictionary(dictionary, "Key", "desc");

            // Assert
            Assert.Equal('c', result.First().Key);
            Assert.Equal('a', result.Last().Key);
        }

        // Add more test methods for different scenarios
    }
}