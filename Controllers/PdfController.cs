using Microsoft.AspNetCore.Mvc;

namespace Bachelor.Controllers;

public class PdfController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}