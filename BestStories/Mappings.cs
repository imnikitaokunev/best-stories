using BestStories.Domain;
using BestStories.HackerRankApi;
using Mapster;

namespace BestStories;

public class Mappings
{
    public static void Apply()
    {
        TypeAdapterConfig<HackerRankStory, Story>
            .NewConfig()
            .Map(dst => dst.Title, src => src.Title)
            .Map(dst => dst.Uri, src => src.Url)
            .Map(dst => dst.PostedBy, src => src.By)
            .Map(dst => dst.Time, src => src.Time)
            .Map(dst => dst.Score, src => src.Score)
            .Map(dst => dst.CommentCount, src => src.Descendants);
    }
}