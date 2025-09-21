using SysHeaders = System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

using SetLiszt.Web.Models;
using SetLiszt.Web.Data;
using SetLiszt.Web.Services;

namespace SetLiszt.Web.Controllers;

[ApiController]
[Route("api/songs")]
public class SongController : ControllerBase {
    private readonly SetLisztDbContext _dbContext;
    private readonly FileDownloadHelper _fileHelper;

    public SongController(
        SetLisztDbContext dbContext,
        FileDownloadHelper fileHelper
    ) {
        _dbContext = dbContext;
        _fileHelper = fileHelper;
    }

    [HttpGet("")]
    public async Task<ActionResult<List<Song>>> ListSongs() {
        return Ok(
            await _dbContext.Songs
                .Include(s => s.SongFiles)
                .ToListAsync()
        );
    }

    [HttpGet("file/{songId}/{transposition}")]
    public async Task<IActionResult> GetSongFile(int songId, string transposition) {
        SongFile.Transposition instTrans;
        switch (transposition.ToLower()) {
            case "concert":
                instTrans = SongFile.Transposition.Concert;
                break;
            case "bass":
                instTrans = SongFile.Transposition.Bass;
                break;
            case "bb":
                instTrans = SongFile.Transposition.Bb;
                break;
            case "eb":
                instTrans = SongFile.Transposition.Eb;
                break;
            default:
                // TODO: we should throw an error if the transposition doesn't exist
                instTrans = SongFile.Transposition.Concert;
                break;
        }

        SongFile? songFile = await _dbContext.SongFiles
                                .Where(sf => sf.SongId == songId && sf.InstrumentTransposition == instTrans)
                                .FirstOrDefaultAsync();
        if (songFile == null) {
            return NotFound();
        }

        var disposition = new SysHeaders.ContentDispositionHeaderValue("inline");
        Response.Headers[HeaderNames.ContentDisposition] = disposition.ToString();
        FileResult? result = _fileHelper.GetSongFile(songFile);
        if (result == null) {
            return NotFound();
        }

        return result;
    }
}
