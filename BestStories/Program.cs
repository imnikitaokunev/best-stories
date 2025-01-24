using BestStories;
using BestStories.HackerRankApi;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.RegisterHackerRankApi(builder.Configuration);

builder.Services.AddScoped<HttpClient>();
builder.Services.AddSingleton<IStoriesCache, StoriesCache>();

var config = TypeAdapterConfig.GlobalSettings;

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

Mappings.Apply();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();