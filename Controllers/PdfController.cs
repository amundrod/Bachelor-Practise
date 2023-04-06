using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Bachelor.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PdfController : Controller
{
    private readonly PdfIndexingService _indexingService;
    private readonly PdfSearchService _searchService;

    public PdfController(PdfIndexingService indexingService, PdfSearchService searchService)
    {
        _indexingService = indexingService;
        _searchService = searchService;
    }

    [HttpPost]
    public IActionResult Index([FromBody] string filePath)
    {
        BackgroundJob.Enqueue(() => _indexingService.IndexPdf(filePath));
        return Ok();
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string searchText)
    {
        var results = _searchService.Search(searchText);
        return Ok(results);
    }
}

