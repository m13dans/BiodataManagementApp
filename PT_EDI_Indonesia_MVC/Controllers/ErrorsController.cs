using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BiodataManagement.Controllers;

[Route("error")]
public class ErrorsController : Controller
{
    private readonly ILogger<ErrorsController> _logger;

    public ErrorsController(ILogger<ErrorsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{statusCode}")]
    public IActionResult Error(int statusCode)
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionFeature != null)
        {
            ViewBag.ErrorMessage = exceptionFeature.Error.Message;
            ViewBag.RouteOfException = exceptionFeature.Path;
        }

        return statusCode switch
        {
            400 => View("BadRequest"),
            404 => View("NotFound"),
            403 => View("Forbidden"),
            _ => View("ServerError")
        };
    }

    // [HttpGet("404")]
    // public IActionResult PageNotFound()
    // {
    //     string originalPath = "unknown";
    //     if (HttpContext.Items.ContainsKey("originalPath"))
    //     {
    //         originalPath = HttpContext.Items["originalPath"] as string;
    //     }
    //     return View();

    // }
    // [HttpGet("403")]
    // public IActionResult Forbiden()
    // {
    //     string originalPath = "unknown";
    //     if (HttpContext.Items.ContainsKey("originalPath"))
    //     {
    //         originalPath = HttpContext.Items["originalPath"] as string;
    //     }
    //     return View();

    // }
    // public IActionResult HandleErrorCode(int statusCode)
    // {
    //     var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

    //     switch (statusCode)
    //     {
    //         case 404:
    //             ViewBag.ErrorMessage = "Sorry the page you requested could not be found";
    //             // ViewBag.RouteOfException = statusCodeData.OriginalPath;
    //             return View();

    //         case 403:
    //             ViewBag.ErrorMessage = "Sorry you don't have access to this page";
    //             // ViewBag.RouteOfException = statusCodeData!.OriginalPath;
    //             return View("403");
    //     }
    //     return View();
    // }
}
