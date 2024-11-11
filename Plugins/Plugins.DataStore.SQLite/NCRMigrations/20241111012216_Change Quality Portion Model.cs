using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plugins.DataStore.SQLite.NCRMigrations
{
    /// <inheritdoc />
    public partial class ChangeQualityPortionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngPortion_RoleReps_RoleRepID",
                table: "EngPortion");

            migrationBuilder.DropForeignKey(
                name: "FK_NCRLog_EngPortion_EngPortionID",
                table: "NCRLog");

            migrationBuilder.DropForeignKey(
                name: "FK_QualityPortions_RoleReps_RoleRepID",
                table: "QualityPortions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EngPortion",
                table: "EngPortion");

            migrationBuilder.RenameTable(
                name: "EngPortion",
                newName: "EngPortions");

            migrationBuilder.RenameIndex(
                name: "IX_EngPortion_RoleRepID",
                table: "EngPortions",
                newName: "IX_EngPortions_RoleRepID");

            migrationBuilder.AlterColumn<int>(
                name: "RoleRepID",
                table: "QualityPortions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "RepID",
                table: "QualityPortions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EngPortions",
                table: "EngPortions",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EngPortions_RoleReps_RoleRepID",
                table: "EngPortions",
                column: "RoleRepID",
                principalTable: "RoleReps",
                principalColumn: "RoleRepID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NCRLog_EngPortions_EngPortionID",
                table: "NCRLog",
                column: "EngPortionID",
                principalTable: "EngPortions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QualityPortions_RoleReps_RoleRepID",
                table: "QualityPortions",
                column: "RoleRepID",
                principalTable: "RoleReps",
                principalColumn: "RoleRepID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngPortions_RoleReps_RoleRepID",
                table: "EngPortions");

            migrationBuilder.DropForeignKey(
                name: "FK_NCRLog_EngPortions_EngPortionID",
                table: "NCRLog");

            migrationBuilder.DropForeignKey(
                name: "FK_QualityPortions_RoleReps_RoleRepID",
                table: "QualityPortions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EngPortions",
                table: "EngPortions");

            migrationBuilder.DropColumn(
                name: "RepID",
                table: "QualityPortions");

            migrationBuilder.RenameTable(
                name: "EngPortions",
                newName: "EngPortion");

            migrationBuilder.RenameIndex(
                name: "IX_EngPortions_RoleRepID",
                table: "EngPortion",
                newName: "IX_EngPortion_RoleRepID");

            migrationBuilder.AlterColumn<int>(
                name: "RoleRepID",
                table: "QualityPortions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EngPortion",
                table: "EngPortion",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EngPortion_RoleReps_RoleRepID",
                table: "EngPortion",
                column: "RoleRepID",
                principalTable: "RoleReps",
                principalColumn: "RoleRepID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NCRLog_EngPortion_EngPortionID",
                table: "NCRLog",
                column: "EngPortionID",
                principalTable: "EngPortion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QualityPortions_RoleReps_RoleRepID",
                table: "QualityPortions",
                column: "RoleRepID",
                principalTable: "RoleReps",
                principalColumn: "RoleRepID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
