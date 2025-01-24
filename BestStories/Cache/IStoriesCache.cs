using BestStories.Domain;

namespace BestStories.Cache;

public interface IStoriesCache
{
    bool Contains(int id);
    Story Get(int id);
    void Add(Story story);
}