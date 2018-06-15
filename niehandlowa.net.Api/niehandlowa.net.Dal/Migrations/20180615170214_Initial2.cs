using Microsoft.EntityFrameworkCore.Migrations;

namespace niehandlowa.net.Dal.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_POIEntities_POIId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "OpeningHoursId",
                table: "POIEntities");

            migrationBuilder.AlterColumn<int>(
                name: "POIId",
                table: "OpeningHours",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_POIEntities_POIId",
                table: "OpeningHours",
                column: "POIId",
                principalTable: "POIEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_POIEntities_POIId",
                table: "OpeningHours");

            migrationBuilder.AddColumn<int>(
                name: "OpeningHoursId",
                table: "POIEntities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "POIId",
                table: "OpeningHours",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_POIEntities_POIId",
                table: "OpeningHours",
                column: "POIId",
                principalTable: "POIEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
