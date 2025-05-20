using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class Addedprofilepath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketUser_Event_EventId",
                table: "TicketUser");

            migrationBuilder.DropIndex(
                name: "IX_TicketUser_EventId",
                table: "TicketUser");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "TicketUser");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "TicketUser",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketUser_EventId",
                table: "TicketUser",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUser_Event_EventId",
                table: "TicketUser",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "event_id");
        }
    }
}
