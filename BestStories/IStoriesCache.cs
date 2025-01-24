using BestStories.Domain;

namespace BestStories;

public interface IStoriesCache
{
    bool Contains(int id);
    Story Get(int id);
}