using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SetLiszt.Web.Models;

public class Song {
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }
    public string? Artist { get; set; }

    public ICollection<SongFile> SongFiles { get; } = new List<SongFile>();

    public List<Set> Sets { get; set; } = [];
    public List<Project> Projects { get; set; } = [];
}
