using BestStories.Services;
using Microsoft.AspNetCore.Mvc;

namespace BestStories.Controllers;

[ApiController]
[Route("api/beststories")]
public class BestStoriesController : ControllerBase
{
    private readonly IBestStoriesService _bestStoriesService;

    public BestStoriesController(IBestStoriesService bestStoriesService)
    {
        _bestStoriesService = bestStoriesService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int count)
    {
        var response = await _bestStoriesService.GetStoriesAsync(count);
        if (!response.IsSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Error);
        }
        
        return Ok(response.Result);
    }
}