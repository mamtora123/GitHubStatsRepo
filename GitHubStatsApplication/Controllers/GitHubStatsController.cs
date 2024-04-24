using GitHubStatsApplication.Interfaces;
using GitHubStatsApplication.Request;
using Microsoft.AspNetCore.Mvc;

namespace GitHubStatsApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubStatsController : ControllerBase
    {
        private readonly IGitHubStatsService _gitHubStatsService;

        public GitHubStatsController(IGitHubStatsService gitHubStatsService)
        {
            _gitHubStatsService = gitHubStatsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLetterFrequency([FromQuery] LetterFrequencyRequest request)
        {
            if (string.IsNullOrEmpty(request.OrderBy))
            {
                return BadRequest(); // Return BadRequestResult for invalid request
            }
            try
            {
                var response = await _gitHubStatsService.GetLetterFrequency(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
