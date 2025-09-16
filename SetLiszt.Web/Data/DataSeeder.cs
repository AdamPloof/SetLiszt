using SetLiszt.Web.Models;

namespace SetLiszt.Web.Data;

public static class DataSeeder {
    public static async Task SeedSongsAsync(SetLisztDbContext context) {
        if (context.Songs.Any()) {
            return;
        }

        Dictionary<string, string> tunes = new Dictionary<string, string>() {
            {"Summertime", "Gershwin"},
            {"Yesterdays", "Kern"},
            {"Blue Monk", "Monk"},
        };

        Dictionary<string, string> files = new() {
            {"Summertime", "summertime.pdf"},
            {"Yesterdays", "yesterdays.pdf"},
            {"Blue Monk", "blue_monk.pdf"},
        };

        foreach (KeyValuePair<string, string> tune in tunes) {
            var song = new Song() {
                Title = tune.Key,
                Artist = tune.Value,
            };

            var file = new SongFile() {
                OriginalFileName = files[song.Title],
                Filepath = files[song.Title],
                InstrumentTransposition = SongFile.Transposition.Concert
            };
            song.SongFiles.Add(file);
            await context.Songs.AddAsync(song);
        }

        await context.SaveChangesAsync();
    }
}
