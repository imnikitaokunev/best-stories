using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestStories.HackerRankApi;

public static class HackerRankModule
{
    public static IServiceCollection RegisterHackerRankApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IHackerRankApi, HackerRankApi>();
        
        var settings = new HackerRankSettings();
        configuration.Bind(nameof(HackerRankSettings), settings);
        services.AddSingleton(settings);

        return services;
    }
}