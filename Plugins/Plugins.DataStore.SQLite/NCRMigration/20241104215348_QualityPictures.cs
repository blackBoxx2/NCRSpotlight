using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plugins.DataStore.SQLite.NCRMigration
{
    /// <inheritdoc />
    public partial class QualityPictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefectPicture",
                table: "QualityPortions");

            migrationBuilder.AddColumn<int>(
                name: "QualityPortionID",
                table: "UploadedFile",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFile_QualityPortionID",
                table: "UploadedFile",
                column: "QualityPortionID");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedFile_QualityPortions_QualityPortionID",
                table: "UploadedFile",
                column: "QualityPortionID",
                principalTable: "QualityPortions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadedFile_QualityPortions_QualityPortionID",
                table: "UploadedFile");

            migrationBuilder.DropIndex(
                name: "IX_UploadedFile_QualityPortionID",
                table: "UploadedFile");

            migrationBuilder.DropColumn(
                name: "QualityPortionID",
                table: "UploadedFile");

            migrationBuilder.AddColumn<byte[]>(
                name: "DefectPicture",
                table: "QualityPortions",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
