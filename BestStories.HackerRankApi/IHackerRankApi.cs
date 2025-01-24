namespace BestStories.HackerRankApi;

public interface IHackerRankApi
{
    Task<Response<IEnumerable<int>>> GetBestStoriesIdsAsync();
    Task<Response<IEnumerable<HackerRankStory>>> GetStoriesAsync(IEnumerable<int> ids);
}