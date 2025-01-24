using BestStories.Domain;
using BestStories.HackerRankApi;
using Microsoft.AspNetCore.Mvc;

namespace BestStories.Controllers;

[ApiController]
[Route("api/beststories")]
public class BestStoriesController : ControllerBase
{
    private readonly IHackerRankApi _hackerRankApi;
    private readonly IStoriesCache _cache;

    public BestStoriesController(IHackerRankApi hackerRankApi, IStoriesCache cache)
    {
        _hackerRankApi = hackerRankApi;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int count)
    {
        var bestStoriesResponse = await _hackerRankApi.GetBestStoriesIdsAsync();
        if (!bestStoriesResponse.IsSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, bestStoriesResponse.Error);
        }

        var response = new List<Story>();

        var bestStories = bestStoriesResponse.Result.Take(count).ToList();
        var cachedStories = bestStories.Where(x => _cache.Contains(x)).Select(id => _cache.Get(id));
        
        var storiesToLoad = bestStories.Except(cachedStories.Select(x => x.Id));
        var loadStoriesResponse = await _hackerRankApi.GetStoriesAsync(storiesToLoad);
        if (!loadStoriesResponse.IsSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, loadStoriesResponse.Error);
        }
        
        // map stories
        
        //response.AddRange(loadStoriesResponse.Result);
        
        return Ok(bestStoriesResponse.Result);
    }
}