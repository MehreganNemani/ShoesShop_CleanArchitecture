using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Persistance.Migrations
{
    public partial class Initial_AddSeedDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Size_SizeID",
                table: "ProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Size",
                table: "Size");

            migrationBuilder.RenameTable(
                name: "Size",
                newName: "Sizes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagTitle",
                table: "Tags",
                column: "TagTitle",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Sizes_SizeID",
                table: "ProductSize",
                column: "SizeID",
                principalTable: "Sizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Sizes_SizeID",
                table: "ProductSize");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagTitle",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes");

            migrationBuilder.RenameTable(
                name: "Sizes",
                newName: "Size");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Size",
                table: "Size",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Size_SizeID",
                table: "ProductSize",
                column: "SizeID",
                principalTable: "Size",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
