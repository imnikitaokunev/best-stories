using System.Collections.Concurrent;
using BestStories.Domain;

namespace BestStories.Cache;

public class StoriesCache : IStoriesCache
{
    private readonly ConcurrentDictionary<int, Story> _stories = new();
    
    public bool Contains(int id)
    {
        return _stories.ContainsKey(id);
    }

    public Story Get(int id)
    {
        return _stories[id];
    }

    public void Add(Story story)
    {
        _stories.TryAdd(story.Id, story);
    }
}