using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class Changedcheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Users_user_id",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_user_id",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "Checkout");

            migrationBuilder.AddColumn<int>(
                name: "checkout_id",
                table: "Event",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Checkout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "event_id",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "Checkout",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "Checkout",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Checkout",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Event_checkout_id",
                table: "Event",
                column: "checkout_id");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_event_id",
                table: "Checkout",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_UsersId",
                table: "Checkout",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_Event_event_id",
                table: "Checkout",
                column: "event_id",
                principalTable: "Event",
                principalColumn: "event_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_Users_UsersId",
                table: "Checkout",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Checkout_checkout_id",
                table: "Event",
                column: "checkout_id",
                principalTable: "Checkout",
                principalColumn: "checkout_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Event_event_id",
                table: "Checkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Users_UsersId",
                table: "Checkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Checkout_checkout_id",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_checkout_id",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_event_id",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_UsersId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "checkout_id",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "location",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Checkout");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "Checkout",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

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
    }
}
