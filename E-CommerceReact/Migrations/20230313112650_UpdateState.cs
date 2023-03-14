using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceReact.Migrations
{
    /// <inheritdoc />
    public partial class UpdateState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "UserAddress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_State",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_State",
                table: "Orders");
        }
    }
}
