using Microsoft.EntityFrameworkCore.Migrations;

namespace niehandlowa.net.Dal.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "POIEntities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "POIEntities",
                nullable: true);
        }
    }
}
