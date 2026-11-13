using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Persistance.Migrations
{
    public partial class Initial_EndOfFirstPhase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Products_ProductID",
                table: "ProductSize");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Sizes_SizeID",
                table: "ProductSize");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductID",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagID",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSize",
                table: "ProductSize");

            migrationBuilder.RenameTable(
                name: "ProductTag",
                newName: "ProductTags");

            migrationBuilder.RenameTable(
                name: "ProductSize",
                newName: "ProductSizes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_TagID",
                table: "ProductTags",
                newName: "IX_ProductTags_TagID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_ProductID",
                table: "ProductTags",
                newName: "IX_ProductTags_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSize_SizeID",
                table: "ProductSizes",
                newName: "IX_ProductSizes_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSize_ProductID",
                table: "ProductSizes",
                newName: "IX_ProductSizes_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Products_ProductID",
                table: "ProductSizes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Sizes_SizeID",
                table: "ProductSizes",
                column: "SizeID",
                principalTable: "Sizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Products_ProductID",
                table: "ProductTags",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagID",
                table: "ProductTags",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Products_ProductID",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Sizes_SizeID",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Products_ProductID",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagID",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes");

            migrationBuilder.RenameTable(
                name: "ProductTags",
                newName: "ProductTag");

            migrationBuilder.RenameTable(
                name: "ProductSizes",
                newName: "ProductSize");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_TagID",
                table: "ProductTag",
                newName: "IX_ProductTag_TagID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_ProductID",
                table: "ProductTag",
                newName: "IX_ProductTag_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_SizeID",
                table: "ProductSize",
                newName: "IX_ProductSize_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_ProductID",
                table: "ProductSize",
                newName: "IX_ProductSize_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSize",
                table: "ProductSize",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Products_ProductID",
                table: "ProductSize",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Sizes_SizeID",
                table: "ProductSize",
                column: "SizeID",
                principalTable: "Sizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductID",
                table: "ProductTag",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagID",
                table: "ProductTag",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
