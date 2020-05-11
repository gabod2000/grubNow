using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddManyTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Areas_AreaId",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Cuisines_CuisineId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CuisineId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "Vendors");

            migrationBuilder.CreateTable(
                name: "VendorWithAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(nullable: true),
                    VendorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorWithAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorWithAreas_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorWithAreas_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorWithCuisine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuisineId = table.Column<int>(nullable: true),
                    VendorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorWithCuisine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorWithCuisine_Cuisines_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Cuisines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorWithCuisine_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorWithAreas_AreaId",
                table: "VendorWithAreas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorWithAreas_VendorId",
                table: "VendorWithAreas",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorWithCuisine_VendorId",
                table: "VendorWithCuisine",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorWithAreas");

            migrationBuilder.DropTable(
                name: "VendorWithCuisine");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CuisineId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CuisineId",
                table: "Vendors",
                column: "CuisineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Areas_AreaId",
                table: "Vendors",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Cuisines_CuisineId",
                table: "Vendors",
                column: "CuisineId",
                principalTable: "Cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
