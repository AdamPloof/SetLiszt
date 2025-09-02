using System.ComponentModel.DataAnnotations;

namespace SetLiszt.Web.Models;

public class Song {
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }
    public string? Artist { get; set; }
    public string? Filepath { get; set; }
    public List<Set> Sets { get; set; } = [];
    public List<Project> Projects { get; set; } = [];

    public enum Transposition {
        Concert,
        Bass,
        Bb,
        Eb
    }
}
