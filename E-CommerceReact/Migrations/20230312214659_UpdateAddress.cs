using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceReact.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Phone",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "UserAddress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Phone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
