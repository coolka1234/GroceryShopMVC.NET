using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProject.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "ImagePath", "Name", "Price", "PriceAsString" },
                values: new object[] { 1, "image/cheese.jpg", "Cheese", 3.0, "3.0" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "ImagePath", "Name", "Price", "PriceAsString" },
                values: new object[] { 3, "image/plum.png", "Plum", 2.7000000476837158, "2.7" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "ImagePath", "Name" },
                values: new object[] { 2, "image/potato.jpg", "Potato" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "ImagePath", "Name", "Price", "PriceAsString" },
                values: new object[] { 3, "image/apple.jpg", "Apple", 2.5, "2.5" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "ImagePath", "Name", "Price", "PriceAsString" },
                values: new object[] { 2, "image/carrot.png", "Carrot", 3.0, "3.0" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "ImagePath", "Name" },
                values: new object[] { 1, "image/milk.jpg", "Milk" });
        }
    }
}
