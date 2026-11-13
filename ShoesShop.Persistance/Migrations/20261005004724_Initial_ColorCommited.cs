using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Persistance.Migrations
{
    public partial class Initial_ColorCommited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Colors_ColorID",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Products_ProductID",
                table: "ProductColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColor",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_ProductID",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "ProductColor");

            migrationBuilder.RenameTable(
                name: "ProductColor",
                newName: "ProductColors");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColor_ColorID",
                table: "ProductColors",
                newName: "IX_ProductColors_ColorID");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductModel",
                table: "Products",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Products",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                columns: new[] { "ProductID", "ColorID" });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "ID", "CreateDate", "IsDeleted", "LastUpdateDate", "SizeNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 35 },
                    { 2, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 36 },
                    { 3, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 37 },
                    { 4, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 38 },
                    { 5, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 39 },
                    { 6, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 40 },
                    { 7, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 41 },
                    { 8, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 42 },
                    { 9, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 43 },
                    { 10, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 44 },
                    { 11, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 45 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorName",
                table: "Colors",
                column: "ColorName",
                unique: true,
                filter: "[IsDeleted] = 0");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Colors_ColorID",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Products_ProductID",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_Colors_ColorName",
                table: "Colors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.RenameTable(
                name: "ProductColors",
                newName: "ProductColor");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ColorID",
                table: "ProductColor",
                newName: "IX_ProductColor_ColorID");

            migrationBuilder.AlterColumn<double>(
                name: "ProductPrice",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ProductModel",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ProductColor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductColor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductColor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "ProductColor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColor",
                table: "ProductColor",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductID",
                table: "ProductColor",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Colors_ColorID",
                table: "ProductColor",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Products_ProductID",
                table: "ProductColor",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
