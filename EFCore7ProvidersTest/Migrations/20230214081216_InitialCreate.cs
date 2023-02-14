using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore7ProvidersTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "test_text");

            migrationBuilder.CreateTable(
                name: "StudyYears",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyYears",
                schema: "test_text",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyYears_StudyYears_Id",
                        column: x => x.Id,
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyPeriods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudyYearId = table.Column<long>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPeriods_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalSchema: "test_text",
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyPeriods_StudyYearId",
                table: "StudyPeriods",
                column: "StudyYearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyPeriods");

            migrationBuilder.DropTable(
                name: "StudyYears",
                schema: "test_text");

            migrationBuilder.DropTable(
                name: "StudyYears");
        }
    }
}
