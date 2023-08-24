using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Bookstore_API.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartFlagForOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOrdered",
                table: "ShoppingCart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrdered",
                table: "ShoppingCart");
        }
    }
}
