using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plugins.DataStore.SQLite.NCRMigrations
{
    /// <inheritdoc />
    public partial class IncorporateIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngPortions_RoleReps_RoleRepID",
                table: "EngPortions");

            migrationBuilder.DropForeignKey(
                name: "FK_QualityPortions_RoleReps_RoleRepID",
                table: "QualityPortions");

            migrationBuilder.DropTable(
                name: "RoleReps");

            migrationBuilder.DropTable(
                name: "Representatives");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_QualityPortions_RoleRepID",
                table: "QualityPortions");

            migrationBuilder.DropIndex(
                name: "IX_EngPortions_RoleRepID",
                table: "EngPortions");

            migrationBuilder.DropColumn(
                name: "RoleRepID",
                table: "QualityPortions");

            migrationBuilder.DropColumn(
                name: "RoleRepID",
                table: "EngPortions");

            migrationBuilder.AddColumn<string>(
                name: "RepID",
                table: "EngPortions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepID",
                table: "EngPortions");

            migrationBuilder.AddColumn<int>(
                name: "RoleRepID",
                table: "QualityPortions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleRepID",
                table: "EngPortions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Representatives",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleInitial = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representatives", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleReps",
                columns: table => new
                {
                    RoleRepID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RepresentativeID = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleReps", x => x.RoleRepID);
                    table.ForeignKey(
                        name: "FK_RoleReps_Representatives_RepresentativeID",
                        column: x => x.RepresentativeID,
                        principalTable: "Representatives",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RoleReps_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QualityPortions_RoleRepID",
                table: "QualityPortions",
                column: "RoleRepID");

            migrationBuilder.CreateIndex(
                name: "IX_EngPortions_RoleRepID",
                table: "EngPortions",
                column: "RoleRepID");

            migrationBuilder.CreateIndex(
                name: "IX_Representatives_FirstName_MiddleInitial_LastName",
                table: "Representatives",
                columns: new[] { "FirstName", "MiddleInitial", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleReps_RepresentativeID",
                table: "RoleReps",
                column: "RepresentativeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleReps_RoleID",
                table: "RoleReps",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EngPortions_RoleReps_RoleRepID",
                table: "EngPortions",
                column: "RoleRepID",
                principalTable: "RoleReps",
                principalColumn: "RoleRepID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QualityPortions_RoleReps_RoleRepID",
                table: "QualityPortions",
                column: "RoleRepID",
                principalTable: "RoleReps",
                principalColumn: "RoleRepID");
        }
    }
}
