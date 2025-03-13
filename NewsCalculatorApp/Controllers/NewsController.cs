using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
namespace NewsCalculatorApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController(Services.NewsService newsService) : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<DTOs.ScoreOutput> Post([FromBody] DTOs.NewsInput newsInput)
    {
        try
        {
            return Ok(new DTOs.ScoreOutput(newsService.GetNewsScore(newsInput)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("limits")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<DTOs.ScoreLimits> GetLimits()
    {
        return Ok(newsService.GetScoreLimits());
    }
}
