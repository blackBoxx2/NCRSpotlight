using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NCRSPOTLIGHT.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationNCRContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Representatives",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleInitial = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupplierName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleReps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleID = table.Column<int>(type: "INTEGER", nullable: false),
                    RepresentativeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleReps", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupplierID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Picture = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QualityPortions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityDefective = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DefectPicture = table.Column<byte[]>(type: "BLOB", nullable: false),
                    DefectDescription = table.Column<string>(type: "TEXT", nullable: false),
                    RoleRepID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityPortions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QualityPortions_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualityPortions_RoleReps_RoleRepID",
                        column: x => x.RoleRepID,
                        principalTable: "RoleReps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCRLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    QualityPortionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCRLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NCRLog_QualityPortions_QualityPortionID",
                        column: x => x.QualityPortionID,
                        principalTable: "QualityPortions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCRLogHistory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChangedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: false),
                    NCRLogID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCRLogHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NCRLogHistory_NCRLog_NCRLogID",
                        column: x => x.NCRLogID,
                        principalTable: "NCRLog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NCRLog_QualityPortionID",
                table: "NCRLog",
                column: "QualityPortionID");

            migrationBuilder.CreateIndex(
                name: "IX_NCRLogHistory_NCRLogID",
                table: "NCRLogHistory",
                column: "NCRLogID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductNumber",
                table: "Products",
                column: "ProductNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID_Description",
                table: "Products",
                columns: new[] { "SupplierID", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualityPortions_ProductID",
                table: "QualityPortions",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_QualityPortions_RoleRepID",
                table: "QualityPortions",
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

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierName",
                table: "Suppliers",
                column: "SupplierName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NCRLogHistory");

            migrationBuilder.DropTable(
                name: "NCRLog");

            migrationBuilder.DropTable(
                name: "QualityPortions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RoleReps");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Representatives");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
