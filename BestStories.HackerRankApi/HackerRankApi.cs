using System.Net.Http.Json;
using BestStories.Common;

namespace BestStories.HackerRankApi;

public class HackerRankApi : IHackerRankApi
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public HackerRankApi(HttpClient httpClient, HackerRankSettings options)
    {
        _httpClient = httpClient;
        _apiUrl = options.Url;
    }

    public async Task<Response<IEnumerable<int>>> GetBestStoriesIdsAsync()
    {
        var httpResponse = await _httpClient.GetAsync($"{_apiUrl}/beststories.json");
        if (!httpResponse.IsSuccessStatusCode)
        {
            return Response<IEnumerable<int>>.Failed($"Http request failed with status code {httpResponse.StatusCode}");
        }

        var response = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<int>>();
        return Response<IEnumerable<int>>.Success(response);
    }

    public async Task<Response<IEnumerable<HackerRankStory>>> GetStoriesAsync(IEnumerable<int> ids)
    {
        var responses = await Task.WhenAll(ids.Select(id => _httpClient.GetAsync($"{_apiUrl}/item/{id}.json")));
        if (responses.Any(r => !r.IsSuccessStatusCode))
        {
            return Response<IEnumerable<HackerRankStory>>.Failed("Http request failed");
        }
        
        var stories = await Task.WhenAll(responses.Select(r => r.Content.ReadFromJsonAsync<HackerRankStory>()));
        return Response<IEnumerable<HackerRankStory>>.Success(stories);
    }
}