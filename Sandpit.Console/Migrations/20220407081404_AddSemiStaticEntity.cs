using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandpit.Console.Migrations
{
    public partial class AddSemiStaticEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SemiStaticEntityOwner",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemiStaticEntityOwner", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "SemiStaticEntityOwner",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Owner1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemiStaticEntityOwner");
        }
    }
}
