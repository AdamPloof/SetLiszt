using SetLiszt.Web.Models;

namespace SetLiszt.Web.Data;

public static class DataSeeder {
    public static async Task SeedSongsAsync(SetLisztDbContext context) {
        if (context.Songs.Any()) {
            return;
        }

        List<string> titles = ["Summertime", "Yesterdays", "Blue Monk"];
        foreach (string title in titles) {
            await context.Songs.AddAsync(new Song() {
                Title = title,
                Artist = "",
                OriginalFileName = "",
                Filepath = "",
                InstrumentTransposition = Song.Transposition.Concert
            });
        }

        await context.SaveChangesAsync();
    }
}
