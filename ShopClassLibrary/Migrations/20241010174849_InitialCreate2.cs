using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Products",
                table: "Products",
                newName: "Name_Product");

            migrationBuilder.AddColumn<long>(
                name: "Product_IdId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_Category = table.Column<string>(type: "text", nullable: false),
                    Category_Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product_category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_Product_CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_category_Category_Name_Product_CategoryId",
                        column: x => x.Name_Product_CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Product_IdId",
                table: "Products",
                column: "Product_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_category_Name_Product_CategoryId",
                table: "Product_category",
                column: "Name_Product_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Product_category_Product_IdId",
                table: "Products",
                column: "Product_IdId",
                principalTable: "Product_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Product_category_Product_IdId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Product_category");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Products_Product_IdId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Product_IdId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Name_Product",
                table: "Products",
                newName: "Products");
        }
    }
}
