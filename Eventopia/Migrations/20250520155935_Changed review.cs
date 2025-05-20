using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class Changedreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Event_event_id",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "event_id",
                table: "Review",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Event_event_id",
                table: "Review",
                column: "event_id",
                principalTable: "Event",
                principalColumn: "event_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Event_event_id",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "event_id",
                table: "Review",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Event_event_id",
                table: "Review",
                column: "event_id",
                principalTable: "Event",
                principalColumn: "event_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
