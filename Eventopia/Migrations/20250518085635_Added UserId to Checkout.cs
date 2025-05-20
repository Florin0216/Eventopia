using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIdtoCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Users_UsersId",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_UsersId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Checkout");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Checkout",
                newName: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_user_id",
                table: "Checkout",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_Users_user_id",
                table: "Checkout",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Users_user_id",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_user_id",
                table: "Checkout");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Checkout",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Checkout",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_UsersId",
                table: "Checkout",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_Users_UsersId",
                table: "Checkout",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
