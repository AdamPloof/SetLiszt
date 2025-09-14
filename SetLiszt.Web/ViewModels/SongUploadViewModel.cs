using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

using SetLiszt.Web.Models;

namespace SetLiszt.Web.ViewModels;

/// <summary>
/// View Model for song uploads
/// </summary>
public class SongUploadViewModel {
    [Required]
    public string? Title { get; set; }

    [Required]
    public IFormFile? File { get; set; }

    public string? Artist { get; set; }
    public SongFile.Transposition Transposition { get; set; } = SongFile.Transposition.Concert;
}
