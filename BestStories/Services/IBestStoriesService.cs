using BestStories.Common;
using BestStories.Domain;

namespace BestStories.Services;

public interface IBestStoriesService
{
    Task<Response<IEnumerable<Story>>> GetStoriesAsync(int count);
}