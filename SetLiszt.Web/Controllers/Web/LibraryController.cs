using Microsoft.AspNetCore.Mvc;
using SetLiszt.Web.ViewModels;
using SetLiszt.Web.Services;

namespace SetLiszt.Web.Controllers;

[Route("/library")]
public class LibraryController : Controller {
    [HttpGet("", Name = "Library")]
    public IActionResult List() {
        return View();
    }

    [HttpGet("upload", Name = "UploadToLibrary")]
    public IActionResult Upload() {
        return View(new SongUploadViewModel());
    }

    [HttpPost("upload", Name = "UploadToLibrary")]
    public IActionResult Upload(SongUploadViewModel model, CancellationToken cancellation) {
        if (!ModelState.IsValid) {
            return View(model);
        }

        return View(model);
    }
}
