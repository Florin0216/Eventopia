using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class addedEventUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "TicketUser",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => x.id);
                    table.ForeignKey(
                        name: "FK_EventUser_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Event_event_id",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketUser_EventId",
                table: "TicketUser",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_event_id",
                table: "EventUser",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_Id",
                table: "EventUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUser_Event_EventId",
                table: "TicketUser",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "event_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketUser_Event_EventId",
                table: "TicketUser");

            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropIndex(
                name: "IX_TicketUser_EventId",
                table: "TicketUser");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "TicketUser");
        }
    }
}
