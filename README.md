# GitHub Stats Application

This application connects to the GitHub API to gather statistics on how often each letter is present in JavaScript/TypeScript files of the lodash repository.

1. Clone the repository: https://github.com/mamtora123/GitHubStatsRepo.git
2. Navigate to the project directory:
---cd GitHubStatsApplication
3. Install dependencies:
---dotnet restore
4. Run the application:
---dotnet run

**API Endpoints**
Get Letter Frequency
1. URL: /GitHubStats
2. Method:- GET
3. Query Parameters:-
orderBy- Specify the order field (Key or Value).
4. Example:
---GET /GitHubStats?orderBy=Key

**Usage**
1. Send a GET request to the /GitHubStats endpoint with query parameters to retrieve letter frequency statistics from the lodash repository.
2. you can specify the order field (orderBy) to customize the sorting of the results.

