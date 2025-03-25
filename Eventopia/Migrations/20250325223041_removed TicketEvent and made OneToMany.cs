using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class removedTicketEventandmadeOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketEvent");

            migrationBuilder.AddColumn<int>(
                name: "event_id",
                table: "Ticket",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_event_id",
                table: "Ticket",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Event_event_id",
                table: "Ticket",
                column: "event_id",
                principalTable: "Event",
                principalColumn: "event_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Event_event_id",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_event_id",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "Ticket");

            migrationBuilder.CreateTable(
                name: "TicketEvent",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketEvent", x => new { x.ticket_id, x.event_id });
                    table.ForeignKey(
                        name: "FK_TicketEvent_Event_event_id",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketEvent_Ticket_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketEvent_event_id",
                table: "TicketEvent",
                column: "event_id");
        }
    }
}
