using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plugins.DataStore.SQLite.ncrcontext
{
    /// <inheritdoc />
    public partial class EngMissingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OriginalEngineer",
                table: "EngPortions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginalRevNumber",
                table: "EngPortions",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalEngineer",
                table: "EngPortions");

            migrationBuilder.DropColumn(
                name: "OriginalRevNumber",
                table: "EngPortions");
        }
    }
}
