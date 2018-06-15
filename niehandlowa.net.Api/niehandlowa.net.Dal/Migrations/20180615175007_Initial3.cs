using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace niehandlowa.net.Dal.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desription",
                table: "POIEntities",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "POIEntities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desription",
                table: "POIEntities");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "POIEntities");
        }
    }
}
