using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "size",
                table: "products",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "size",
                table: "products");
        }
    }
}
