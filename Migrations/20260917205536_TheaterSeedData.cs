using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumMovieTheater.Migrations
{
    /// <inheritdoc />
    public partial class TheaterSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "theater",
                columns: new[] { "TheaterId", "Active", "Address", "Description", "Name" },
                values: new object[] { 1, true, "100 S Central Expy", "Our first theater", "Richardson" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "theater",
                keyColumn: "TheaterId",
                keyValue: 1);
        }
    }
}
