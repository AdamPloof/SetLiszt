using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using SetLiszt.Web.Models;
using SetLiszt.Web.Extensions;

namespace SetLiszt.Web.Data;

public class SetLisztDbContext : DbContext {
    public SetLisztDbContext(
        DbContextOptions<SetLisztDbContext> options
    ) : base(options) {}

    public DbSet<Song>     Songs { get; set; } = null!;
    public DbSet<SongFile> SongFiles { get; set; } = null!;
    public DbSet<Project>  Projects { get; set; } = null!;
    public DbSet<Set>      Sets { get; set; } = null!;
    public DbSet<Gig>      Gigs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<Song>().ToTable("song");
        builder.Entity<SongFile>().ToTable("song_file");
        builder.Entity<Project>().ToTable("project");
        builder.Entity<Gig>().ToTable("gig");
        builder.Entity<Set>().ToTable("set");

        foreach (var entity in builder.Model.GetEntityTypes()) {
            // Replace table names
            string? entityTableName = entity.GetTableName();
            if (entityTableName != null) {
                entity.SetTableName(entityTableName.ToSnakeCase());
            }

            // Replace column names            
            foreach (var property in entity.GetProperties()) {
                var declaringEntityType = property.DeclaringType as IMutableEntityType;
                if (declaringEntityType != null) {
                    string? tableName = declaringEntityType.GetTableName();
                    string? columnName = null;
                    if (tableName != null) {
                        columnName = property.GetColumnName(StoreObjectIdentifier.Table(tableName, null));
                    }

                    if (columnName != null) {
                        property.SetColumnName(columnName.ToSnakeCase());
                    }
                }
            }

            foreach (var key in entity.GetKeys()) {
                string? keyName = key.GetName();
                if (keyName != null) {
                    key.SetName(keyName.ToSnakeCase());
                }
            }

            foreach (var key in entity.GetForeignKeys()) {
                string? constraintName = key.GetConstraintName();
                if (constraintName != null) {
                    key.SetConstraintName(constraintName.ToSnakeCase());
                }
            }

            foreach (var index in entity.GetIndexes()) {
                string? indexDbName = index.GetDatabaseName();
                if (indexDbName != null) {
                    index.SetDatabaseName(indexDbName.ToSnakeCase());
                }
            }
        }
    }
}
