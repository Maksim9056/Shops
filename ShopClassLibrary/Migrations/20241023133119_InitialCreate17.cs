using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Description", "StatusName" },
                values: new object[,]
                {
                    { 8L, "Надо соберать", "Новый заказ" },
                    { 9L, "Изменение при  сборки проверять", "Заказ изменен" },
                    { 10L, "Заказ удален его делать не надо!", "Заказ отменен" },
                    { 11L, "Заказ заново соберают", "Заказ переделывают" },
                    { 12L, "Заказ готов можете его забрать!", "Заказ готов" },
                    { 13L, "Заказ оплачен!", "Заказ оплачен" },
                    { 14L, "Заказ сделан!", "Заказ сделан" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 14L);
        }
    }
}
