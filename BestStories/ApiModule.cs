using BestStories.Cache;
using BestStories.Services;
using Mapster;
using MapsterMapper;

namespace BestStories;

public static class ApiModule
{
    public static IServiceCollection RegisterApi(this IServiceCollection services)
    {
        services.AddScoped<HttpClient>();
        services.AddScoped<IBestStoriesService, BestStoriesService>();
        services.AddSingleton<IStoriesCache, StoriesCache>();

        var config = TypeAdapterConfig.GlobalSettings;

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        Mappings.Apply();

        return services;
    }
}