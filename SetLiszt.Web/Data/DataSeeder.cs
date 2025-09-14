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

        foreach (KeyValuePair<string, string> tune in tunes) {
            await context.Songs.AddAsync(new Song() {
                Title = tune.Key,
                Artist = tune.Value,
            });
        }

        await context.SaveChangesAsync();
    }
}
