using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Persistance.Migrations
{
    public partial class Initial_ProductCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Products_ProductID",
                table: "Gallery");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Category_CategoryID",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Products_ProductID",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Colors_ColorID",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Products_ProductID",
                table: "ProductColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_ProductID",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Gallery_ProductID",
                table: "Gallery");

            migrationBuilder.DropColumn(
                name: "GallaryImage",
                table: "Gallery");

            migrationBuilder.AddColumn<bool>(
                name: "ShowIsSlider",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ProductColors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductColors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductColors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "ProductColors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Gallery",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductID1",
                table: "Gallery",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductID",
                table: "ProductColors",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductID_CategoryID",
                table: "ProductCategory",
                columns: new[] { "ProductID", "CategoryID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_ProductID1",
                table: "Gallery",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "UX_Gallery_OneMainPerProduct",
                table: "Gallery",
                column: "ProductID",
                unique: true,
                filter: "[IsMain] = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Products_ProductID",
                table: "Gallery",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Products_ProductID1",
                table: "Gallery",
                column: "ProductID1",
                principalTable: "Products",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Category_CategoryID",
                table: "ProductCategory",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Products_ProductID",
                table: "ProductCategory",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Colors_ColorID",
                table: "ProductColors",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Products_ProductID",
                table: "ProductColors",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Products_ProductID",
                table: "Gallery");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Products_ProductID1",
                table: "Gallery");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Category_CategoryID",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Products_ProductID",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Colors_ColorID",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Products_ProductID",
                table: "ProductColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ProductID",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_ProductID_CategoryID",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Gallery_ProductID1",
                table: "Gallery");

            migrationBuilder.DropIndex(
                name: "UX_Gallery_OneMainPerProduct",
                table: "Gallery");

            migrationBuilder.DropColumn(
                name: "ShowIsSlider",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Gallery");

            migrationBuilder.DropColumn(
                name: "ProductID1",
                table: "Gallery");

            migrationBuilder.AddColumn<string>(
                name: "GallaryImage",
                table: "Gallery",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                columns: new[] { "ProductID", "ColorID" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductID",
                table: "ProductCategory",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_ProductID",
                table: "Gallery",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Products_ProductID",
                table: "Gallery",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Category_CategoryID",
                table: "ProductCategory",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Products_ProductID",
                table: "ProductCategory",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Colors_ColorID",
                table: "ProductColors",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Products_ProductID",
                table: "ProductColors",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
