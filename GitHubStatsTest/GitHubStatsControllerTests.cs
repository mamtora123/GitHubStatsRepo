using GitHubStatsApplication.Controllers;
using GitHubStatsApplication.Interfaces;
using GitHubStatsApplication.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubStatsTest
{
    public class GitHubStatsControllerTests
    {
        [Fact]
        public async Task GetLetterFrequency_Returns_OkResult()
        {
            // Arrange
            var mockService = new Mock<IGitHubStatsService>();
            // Setup mock service behavior if needed

            var controller = new GitHubStatsController(mockService.Object);
            var request = new LetterFrequencyRequest { OrderBy = "Key" };

            // Act
            var result = await controller.GetLetterFrequency(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            // Add more assertions as needed
        }

        [Fact]
        public async Task GetLetterFrequency_Returns_BadRequest_For_InvalidRequest()
        {
            // Arrange
            var mockService = new Mock<IGitHubStatsService>();
            var controller = new GitHubStatsController(mockService.Object);
            var request = new LetterFrequencyRequest(); // No query parameters set

            // Act
            var result = await controller.GetLetterFrequency(request);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
