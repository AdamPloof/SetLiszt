using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SetLiszt.Web.Models;
using SetLiszt.Web.Data;

namespace SetLiszt.Web.Controllers;

[ApiController]
[Route("songs")]
public class SongController : ControllerBase {
    private readonly SetLisztDbContext _dbContext;

    public SongController(SetLisztDbContext dbContext) {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Song>>> ListSongs() {
        return Ok(await _dbContext.Songs.ToListAsync());
    }
}
