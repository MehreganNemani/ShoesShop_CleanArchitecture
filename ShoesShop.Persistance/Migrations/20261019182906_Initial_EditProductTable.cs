using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Persistance.Migrations
{
    public partial class Initial_EditProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowIsSlider",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "ShowInSlider",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCode",
                table: "Products",
                column: "ProductCode",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductModel",
                table: "Products",
                column: "ProductModel",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductModel",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShowInSlider",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "ShowIsSlider",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
