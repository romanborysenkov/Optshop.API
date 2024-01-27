using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "deliveryPrice",
                table: "payments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deliveryPrice",
                table: "payments");
        }
    }
}
