using BestStories.Cache;
using BestStories.Common;
using BestStories.Domain;
using BestStories.HackerRankApi;
using MapsterMapper;

namespace BestStories.Services;

public class BestStoriesService : IBestStoriesService
{
    private readonly IHackerRankApi _hackerRankApi;
    private readonly IStoriesCache _cache;
    private readonly IMapper _mapper;

    public BestStoriesService(IHackerRankApi hackerRankApi, IStoriesCache cache, IMapper mapper)
    {
        _hackerRankApi = hackerRankApi;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<Story>>> GetStoriesAsync(int count)
    {
        var bestStoriesResponse = await _hackerRankApi.GetBestStoriesIdsAsync();
        if (!bestStoriesResponse.IsSuccess)
        {
            return Response<IEnumerable<Story>>.Failed(bestStoriesResponse.Error);
        }

        var bestStoriesIds = bestStoriesResponse.Result.Take(count).ToList();
        var cachedStories = GetCachedStories(bestStoriesIds).ToList();
      
        var storiesToLoad = bestStoriesIds.Except(cachedStories.Select(x => x.Id));
        var loadedStoriesResponse = await LoadAndCacheStoriesAsync(storiesToLoad);
        if (!loadedStoriesResponse.IsSuccess)
        {
            return Response<IEnumerable<Story>>.Failed(loadedStoriesResponse.Error);
        }
        
        var stories = new List<Story>();
        
        stories.AddRange(cachedStories);
        stories.AddRange(loadedStoriesResponse.Result);

        return Response<IEnumerable<Story>>.Success(stories);
    }

    private IEnumerable<Story> GetCachedStories(IEnumerable<int> ids)
    {
        foreach (var id in ids)
        {
            if (_cache.Contains(id))
            {
                yield return _cache.Get(id);
            }
        }
    }
    
    private async Task<Response<IEnumerable<Story>>> LoadAndCacheStoriesAsync(IEnumerable<int> ids)
    {
        var loadStoriesResponse = await _hackerRankApi.GetStoriesAsync(ids);
        if (!loadStoriesResponse.IsSuccess)
        {
            return Response<IEnumerable<Story>>.Failed(loadStoriesResponse.Error);
        }
        
        var stories = _mapper.Map<IEnumerable<Story>>(loadStoriesResponse.Result).ToList();

        foreach (var story in stories)
        {
            if (!_cache.Contains(story.Id))
            {
                _cache.Add(story);
            }
        }
        
        return Response<IEnumerable<Story>>.Success(stories);
    }
}