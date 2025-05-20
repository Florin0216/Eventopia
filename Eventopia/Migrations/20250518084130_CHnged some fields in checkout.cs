using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class CHngedsomefieldsincheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Checkout");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Checkout",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "amount",
                table: "Checkout",
                type: "text",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Checkout",
                newName: "user_id");

            migrationBuilder.AlterColumn<float>(
                name: "amount",
                table: "Checkout",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Checkout",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
