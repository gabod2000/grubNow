using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddVendorWithCoisne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorWithCuisine_Cuisines_VendorId",
                table: "VendorWithCuisine");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorWithCuisine_Vendors_VendorId",
                table: "VendorWithCuisine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorWithCuisine",
                table: "VendorWithCuisine");

            migrationBuilder.RenameTable(
                name: "VendorWithCuisine",
                newName: "VendorWithCuisines");

            migrationBuilder.RenameIndex(
                name: "IX_VendorWithCuisine_VendorId",
                table: "VendorWithCuisines",
                newName: "IX_VendorWithCuisines_VendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorWithCuisines",
                table: "VendorWithCuisines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorWithCuisines_Cuisines_VendorId",
                table: "VendorWithCuisines",
                column: "VendorId",
                principalTable: "Cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorWithCuisines_Vendors_VendorId",
                table: "VendorWithCuisines",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorWithCuisines_Cuisines_VendorId",
                table: "VendorWithCuisines");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorWithCuisines_Vendors_VendorId",
                table: "VendorWithCuisines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorWithCuisines",
                table: "VendorWithCuisines");

            migrationBuilder.RenameTable(
                name: "VendorWithCuisines",
                newName: "VendorWithCuisine");

            migrationBuilder.RenameIndex(
                name: "IX_VendorWithCuisines_VendorId",
                table: "VendorWithCuisine",
                newName: "IX_VendorWithCuisine_VendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorWithCuisine",
                table: "VendorWithCuisine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorWithCuisine_Cuisines_VendorId",
                table: "VendorWithCuisine",
                column: "VendorId",
                principalTable: "Cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorWithCuisine_Vendors_VendorId",
                table: "VendorWithCuisine",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
