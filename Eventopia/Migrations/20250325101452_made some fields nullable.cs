using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventopia.Migrations
{
    /// <inheritdoc />
    public partial class madesomefieldsnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Category_category_id",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Venue_venue_id",
                table: "Event");

            migrationBuilder.AlterColumn<int>(
                name: "venue_id",
                table: "Event",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                table: "Event",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Category_category_id",
                table: "Event",
                column: "category_id",
                principalTable: "Category",
                principalColumn: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Venue_venue_id",
                table: "Event",
                column: "venue_id",
                principalTable: "Venue",
                principalColumn: "venue_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Category_category_id",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Venue_venue_id",
                table: "Event");

            migrationBuilder.AlterColumn<int>(
                name: "venue_id",
                table: "Event",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                table: "Event",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Category_category_id",
                table: "Event",
                column: "category_id",
                principalTable: "Category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Venue_venue_id",
                table: "Event",
                column: "venue_id",
                principalTable: "Venue",
                principalColumn: "venue_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
