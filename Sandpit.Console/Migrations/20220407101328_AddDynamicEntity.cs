using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandpit.Console.Migrations
{
    public partial class AddDynamicEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SemiStaticEntity",
                table: "SemiStaticEntityOwner",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DynamicEntity",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicEntity", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "DynamicEntity",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "DE1" });

            migrationBuilder.InsertData(
                table: "DynamicEntity",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "DE2" });

            migrationBuilder.InsertData(
                table: "DynamicEntity",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "DE3" });

            migrationBuilder.UpdateData(
                table: "SemiStaticEntityOwner",
                keyColumn: "ID",
                keyValue: 1,
                column: "SemiStaticEntity",
                value: "DE1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicEntity");

            migrationBuilder.DropColumn(
                name: "SemiStaticEntity",
                table: "SemiStaticEntityOwner");
        }
    }
}
