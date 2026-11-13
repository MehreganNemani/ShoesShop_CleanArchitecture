using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Persistance.Migrations
{
    public partial class Initial_CreateCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentID",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentID",
                table: "Category",
                column: "ParentID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentID",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Category");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentID",
                table: "Category",
                column: "ParentID",
                principalTable: "Category",
                principalColumn: "ID");
        }
    }
}
