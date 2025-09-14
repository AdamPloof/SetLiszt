using System.ComponentModel.DataAnnotations;

namespace SetLiszt.Web.Models;

public class SongFile {
    [Key]
    public int Id { get; set; }

    [Required]
    public int SongId { get; set; }

    [Required]
    public required string OriginalFileName { get; set; }

    [Required]
    public string? Filepath { get; set; }

    public Transposition InstrumentTransposition { get; set; }

    public enum Transposition {
        Concert,
        Bass,
        Bb,
        Eb
    }
}
