using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCustomerOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "customers",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "customers",
                newName: "orderIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "customers",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "orderIds",
                table: "customers",
                newName: "orderId");
        }
    }
}
