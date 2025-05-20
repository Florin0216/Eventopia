using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class Changedamount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "Checkout",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "amount",
                table: "Checkout",
                type: "text",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
