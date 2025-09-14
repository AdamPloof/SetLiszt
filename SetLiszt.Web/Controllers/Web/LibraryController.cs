using Microsoft.AspNetCore.Mvc;

using SetLiszt.Web.ViewModels;
using SetLiszt.Web.Services;
using SetLiszt.Web.Data;
using SetLiszt.Web.Models;

namespace SetLiszt.Web.Controllers;

[Route("/library")]
public class LibraryController : Controller {
    private FileUploadHelper _uploadHelper;
    private SetLisztDbContext _dbContext;

    public LibraryController(FileUploadHelper uploadHelper, SetLisztDbContext dbContext) {
        _uploadHelper = uploadHelper;
        _dbContext = dbContext;
    }

    [HttpGet("", Name = "Library")]
    public IActionResult List() {
        return View();
    }

    [HttpGet("upload", Name = "UploadToLibrary")]
    public IActionResult Upload() {
        return View(new SongUploadViewModel());
    }

    [ValidateAntiForgeryToken]
    [HttpPost("upload", Name = "UploadToLibrary")]
    public async Task<IActionResult> Upload(SongUploadViewModel model, CancellationToken cancellation) {
        if (!ModelState.IsValid || model.File == null) {
            return View(model);
        }

        string filepath = await _uploadHelper.UploadToLocalStorage(model.File, cancellation);
        SongFile songFile = new SongFile() {
            Filepath = filepath,
            OriginalFileName = FileUploadHelper.GetOriginalFileName(model.File),
            InstrumentTransposition = model.Transposition,
        };
        Song song = new() {
            Title = model.Title!,
            Artist = model.Artist,
        };
        song.SongFiles.Add(songFile);

        _dbContext.Add(song);
        await _dbContext.SaveChangesAsync();

        return RedirectToRoute("Library");
    }
}
