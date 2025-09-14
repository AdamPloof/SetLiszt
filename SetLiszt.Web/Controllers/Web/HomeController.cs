using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using SetLiszt.Web.ViewModels;
using SetLiszt.Web.Configuration;

namespace SetLiszt.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptions<FileUploadOptions> _opts;

    public HomeController(ILogger<HomeController> logger, IOptions<FileUploadOptions> opts) {
        _logger = logger;
        _opts = opts;
    }

    [Route("/", Name = "Home")]
    public IActionResult Index() {
        var model = new DirectoryViewModel() {
            ContentRoot = _opts.Value.RootDirectory,
            WebRoot = _opts.Value.RootDirectory,
        };

        return View(model);
    }

    [Route("/privacy", Name = "Privacy")]
    public IActionResult Privacy() {
        return View();
    }

    [Route("/error", Name = "Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public class DirectoryViewModel {
        public required string ContentRoot { get; set; }
        public required string WebRoot { get; set; }
    }
}
