using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetLiszt.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSongFileMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstrumentTransposition",
                table: "Song",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "Song",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstrumentTransposition",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "Song");
        }
    }
}
