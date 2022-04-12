using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandpit.Console.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "EncapsulateParent1",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_EncapsulateParent1", x => x.ID);
                });

            _ = migrationBuilder.CreateTable(
                name: "EncapsulateParent2",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_EncapsulateParent2", x => x.ID);
                });

            _ = migrationBuilder.CreateTable(
                name: "Parent1",
                columns: table => new
                {
                    EncapsulateParent1ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Parent1", x => x.EncapsulateParent1ID);
                    _ = table.ForeignKey(
                        name: "FK_Parent1_EncapsulateParent1_EncapsulateParent1ID",
                        column: x => x.EncapsulateParent1ID,
                        principalTable: "EncapsulateParent1",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "Child2",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentEncapsulateParent2ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Child2", x => x.ID);
                    _ = table.ForeignKey(
                        name: "FK_Child2_EncapsulateParent2_ParentEncapsulateParent2ID",
                        column: x => x.ParentEncapsulateParent2ID,
                        principalTable: "EncapsulateParent2",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "Child1",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentEncapsulateParent1ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Child1", x => x.ID);
                    _ = table.ForeignKey(
                        name: "FK_Child1_Parent1_ParentEncapsulateParent1ID",
                        column: x => x.ParentEncapsulateParent1ID,
                        principalTable: "Parent1",
                        principalColumn: "EncapsulateParent1ID",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_Child1_ParentEncapsulateParent1ID",
                table: "Child1",
                column: "ParentEncapsulateParent1ID");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Child2_ParentEncapsulateParent2ID",
                table: "Child2",
                column: "ParentEncapsulateParent2ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "Child1");

            _ = migrationBuilder.DropTable(
                name: "Child2");

            _ = migrationBuilder.DropTable(
                name: "Parent1");

            _ = migrationBuilder.DropTable(
                name: "EncapsulateParent2");

            _ = migrationBuilder.DropTable(
                name: "EncapsulateParent1");
        }
    }
}
