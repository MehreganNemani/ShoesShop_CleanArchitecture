using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Persistance.Migrations
{
    public partial class Initial_CreateGalleryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Products_ProductID1",
                table: "Gallery");

            migrationBuilder.DropIndex(
                name: "IX_Gallery_ProductID1",
                table: "Gallery");

            migrationBuilder.DropColumn(
                name: "ProductID1",
                table: "Gallery");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID1",
                table: "Gallery",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_ProductID1",
                table: "Gallery",
                column: "ProductID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Products_ProductID1",
                table: "Gallery",
                column: "ProductID1",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
