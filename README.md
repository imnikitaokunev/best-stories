# Best Stories API

This project is a RESTful API built using ASP.NET Core that retrieves the details of the first `n` "best stories" from the Hacker News API. The API is designed to efficiently fetch and serve story data while minimizing requests to the Hacker News API to handle a large number of user requests.

## Features

- Fetches the top `n` best stories based on their score.
- Sorts stories in descending order of their score.
- Returns story details in the following format:

```json
[
  {
    "title": "A uBlock Origin update was rejected from the Chrome Web Store",
    "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
    "postedBy": "ismaildonmez",
    "time": "2019-10-12T13:43:01+00:00",
    "score": 1716,
    "commentCount": 572
  },
  {
    "..."
  }
]
```

- Caching is implemented to reduce the number of requests made to the Hacker News API, ensuring efficient service during high traffic.

## Prerequisites

To run this project, you will need:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- [Git](https://git-scm.com/)

## How to Run the Application

1. Clone the Repository

   ```bash
   git clone https://github.com/imnikitaokunev/best-stories.git
   cd best-stories
   ```

2. Restore Dependencies

   ```bash
   dotnet restore
   ```

3. Run the Application

   ```bash
   dotnet run
   ```

   By default, the API will be available at `https://localhost:5276` as described in `launchSettings.Json`

4. Access the API

   Use the endpoint `/api/beststories?count=<number>` to fetch the details of the first `n` best stories. For example:

   ```bash
   curl https://localhost:5276/api/beststories?count=5
   ```

   This will return the details of the top 5 best stories.

## Assumptions

- The number of stories (`n`) requested will be within a reasonable range to avoid overwhelming the Hacker News API.
- Story IDs and details are retrieved in real-time and cached for subsequent requests.

## Enhancements and Future Improvements

If given more time, the following enhancements could be implemented:

1. **Distributed Caching**: Use distributed caching systems like Redis or Memcached to handle cases when multiple stories missing in the cache were requested simultaneously.
2. **Rate Limiting**: Implement rate limiting to prevent abuse of the API and to better manage traffic.
3. **Unit and Integration Tests**: Increase test coverage to ensure reliability and robustness.
4. **Advanced Error Handling**: Improve error handling to account for potential edge cases, such as network failures or unexpected API changes from Hacker News.


