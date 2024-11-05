using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plugins.DataStore.SQLite.NCRMigration
{
    /// <inheritdoc />
    public partial class AddQualityPortionToNCRLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualityPortionID",
                table: "NCRLog",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NCRLog_QualityPortionID",
                table: "NCRLog",
                column: "QualityPortionID");

            migrationBuilder.AddForeignKey(
                name: "FK_NCRLog_QualityPortions_QualityPortionID",
                table: "NCRLog",
                column: "QualityPortionID",
                principalTable: "QualityPortions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NCRLog_QualityPortions_QualityPortionID",
                table: "NCRLog");

            migrationBuilder.DropIndex(
                name: "IX_NCRLog_QualityPortionID",
                table: "NCRLog");

            migrationBuilder.DropColumn(
                name: "QualityPortionID",
                table: "NCRLog");
        }
    }
}
