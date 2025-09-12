using Microsoft.EntityFrameworkCore;

using SetLiszt.Web.Models;

namespace SetLiszt.Web.Data;

public class SetLisztDbContext : DbContext {
    public SetLisztDbContext(
        DbContextOptions<SetLisztDbContext> options
    ) : base(options) {}

    public DbSet<Song> Songs { get; set; } = null!;
    public DbSet<Set> Sets { get; set; } = null!;
    public DbSet<Gig> Gigs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Song>().ToTable("Song");
        modelBuilder.Entity<Set>().ToTable("Set");
        modelBuilder.Entity<Gig>().ToTable("Gig");
    }
}
