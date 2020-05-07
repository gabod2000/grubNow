using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class DriverWithAre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Areas_AreaId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_AreaId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Drivers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AreaId",
                table: "Drivers",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Areas_AreaId",
                table: "Drivers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
