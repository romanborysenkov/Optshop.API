using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class CustomerOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userName = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    phoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    country = table.Column<string>(type: "TEXT", nullable: true),
                    city = table.Column<string>(type: "TEXT", nullable: true),
                    streetAddress = table.Column<string>(type: "TEXT", nullable: true),
                    postalCode = table.Column<string>(type: "TEXT", nullable: true),
                    zip_code = table.Column<string>(type: "TEXT", nullable: true),
                    province = table.Column<string>(type: "TEXT", nullable: true),
                    plz = table.Column<string>(type: "TEXT", nullable: true),
                    orderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    productId = table.Column<int>(type: "INTEGER", nullable: false),
                    productCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
