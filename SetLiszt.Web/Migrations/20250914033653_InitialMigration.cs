using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SetLiszt.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gig",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<string>(type: "text", nullable: true),
                    hit_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gig", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "set",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_set", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "song",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    artist = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_song", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gig_project",
                columns: table => new
                {
                    gigs_id = table.Column<int>(type: "integer", nullable: false),
                    projects_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gig_project", x => new { x.gigs_id, x.projects_id });
                    table.ForeignKey(
                        name: "fk_gig_project_gig_gigs_id",
                        column: x => x.gigs_id,
                        principalTable: "gig",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gig_project_project_projects_id",
                        column: x => x.projects_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gig_set",
                columns: table => new
                {
                    gigs_id = table.Column<int>(type: "integer", nullable: false),
                    sets_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gig_set", x => new { x.gigs_id, x.sets_id });
                    table.ForeignKey(
                        name: "fk_gig_set_gig_gigs_id",
                        column: x => x.gigs_id,
                        principalTable: "gig",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gig_set_set_sets_id",
                        column: x => x.sets_id,
                        principalTable: "set",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_set",
                columns: table => new
                {
                    projects_id = table.Column<int>(type: "integer", nullable: false),
                    sets_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_set", x => new { x.projects_id, x.sets_id });
                    table.ForeignKey(
                        name: "fk_project_set_project_projects_id",
                        column: x => x.projects_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_set_set_sets_id",
                        column: x => x.sets_id,
                        principalTable: "set",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_song",
                columns: table => new
                {
                    projects_id = table.Column<int>(type: "integer", nullable: false),
                    songs_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_song", x => new { x.projects_id, x.songs_id });
                    table.ForeignKey(
                        name: "fk_project_song_project_projects_id",
                        column: x => x.projects_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_song_song_songs_id",
                        column: x => x.songs_id,
                        principalTable: "song",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "set_song",
                columns: table => new
                {
                    sets_id = table.Column<int>(type: "integer", nullable: false),
                    songs_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_set_song", x => new { x.sets_id, x.songs_id });
                    table.ForeignKey(
                        name: "fk_set_song_set_sets_id",
                        column: x => x.sets_id,
                        principalTable: "set",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_set_song_song_songs_id",
                        column: x => x.songs_id,
                        principalTable: "song",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "song_file",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    song_id = table.Column<int>(type: "integer", nullable: false),
                    original_file_name = table.Column<string>(type: "text", nullable: false),
                    filepath = table.Column<string>(type: "text", nullable: false),
                    instrument_transposition = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_song_file", x => x.id);
                    table.ForeignKey(
                        name: "fk_song_file_song_song_id",
                        column: x => x.song_id,
                        principalTable: "song",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_gig_project_projects_id",
                table: "gig_project",
                column: "projects_id");

            migrationBuilder.CreateIndex(
                name: "ix_gig_set_sets_id",
                table: "gig_set",
                column: "sets_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_set_sets_id",
                table: "project_set",
                column: "sets_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_song_songs_id",
                table: "project_song",
                column: "songs_id");

            migrationBuilder.CreateIndex(
                name: "ix_set_song_songs_id",
                table: "set_song",
                column: "songs_id");

            migrationBuilder.CreateIndex(
                name: "ix_song_file_song_id",
                table: "song_file",
                column: "song_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gig_project");

            migrationBuilder.DropTable(
                name: "gig_set");

            migrationBuilder.DropTable(
                name: "project_set");

            migrationBuilder.DropTable(
                name: "project_song");

            migrationBuilder.DropTable(
                name: "set_song");

            migrationBuilder.DropTable(
                name: "song_file");

            migrationBuilder.DropTable(
                name: "gig");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "set");

            migrationBuilder.DropTable(
                name: "song");
        }
    }
}
