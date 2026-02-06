using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingHoldDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingHold_TimeSlot_TimeSlotId",
                table: "BookingHold");

            migrationBuilder.DropIndex(
                name: "IX_BookingHold_TimeSlotId",
                table: "BookingHold");

            migrationBuilder.DropColumn(
                name: "IsHeld",
                table: "TimeSlot");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "BookingHold");

            migrationBuilder.AddColumn<int>(
                name: "AvailableCount",
                table: "TimeSlot",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookingHoldDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSlotId = table.Column<long>(type: "bigint", nullable: true),
                    BookingHoldId = table.Column<long>(type: "bigint", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingHoldDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingHoldDetail_BookingHold_BookingHoldId",
                        column: x => x.BookingHoldId,
                        principalTable: "BookingHold",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingHoldDetail_TimeSlot_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingHoldDetail_BookingHoldId",
                table: "BookingHoldDetail",
                column: "BookingHoldId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingHoldDetail_TimeSlotId",
                table: "BookingHoldDetail",
                column: "TimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingHoldDetail");

            migrationBuilder.DropColumn(
                name: "AvailableCount",
                table: "TimeSlot");

            migrationBuilder.AddColumn<bool>(
                name: "IsHeld",
                table: "TimeSlot",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "TimeSlotId",
                table: "BookingHold",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingHold_TimeSlotId",
                table: "BookingHold",
                column: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHold_TimeSlot_TimeSlotId",
                table: "BookingHold",
                column: "TimeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "Id");
        }
    }
}
