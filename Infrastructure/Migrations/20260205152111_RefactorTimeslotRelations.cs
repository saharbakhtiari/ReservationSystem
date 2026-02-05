using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTimeslotRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Space_SpaceId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingHold_Space_SpaceId",
                table: "BookingHold");

            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "BookingHold");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "BookingHold");

            migrationBuilder.RenameColumn(
                name: "SpaceId",
                table: "BookingHold",
                newName: "TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingHold_SpaceId",
                table: "BookingHold",
                newName: "IX_BookingHold_TimeSlotId");

            migrationBuilder.RenameColumn(
                name: "SpaceId",
                table: "Booking",
                newName: "TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_SpaceId",
                table: "Booking",
                newName: "IX_Booking_TimeSlotId");

            migrationBuilder.AddColumn<bool>(
                name: "IsHeld",
                table: "TimeSlot",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_TimeSlot_TimeSlotId",
                table: "Booking",
                column: "TimeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHold_TimeSlot_TimeSlotId",
                table: "BookingHold",
                column: "TimeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_TimeSlot_TimeSlotId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingHold_TimeSlot_TimeSlotId",
                table: "BookingHold");

            migrationBuilder.DropColumn(
                name: "IsHeld",
                table: "TimeSlot");

            migrationBuilder.RenameColumn(
                name: "TimeSlotId",
                table: "BookingHold",
                newName: "SpaceId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingHold_TimeSlotId",
                table: "BookingHold",
                newName: "IX_BookingHold_SpaceId");

            migrationBuilder.RenameColumn(
                name: "TimeSlotId",
                table: "Booking",
                newName: "SpaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_TimeSlotId",
                table: "Booking",
                newName: "IX_Booking_SpaceId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "BookingHold",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "BookingHold",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Space_SpaceId",
                table: "Booking",
                column: "SpaceId",
                principalTable: "Space",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHold_Space_SpaceId",
                table: "BookingHold",
                column: "SpaceId",
                principalTable: "Space",
                principalColumn: "Id");
        }
    }
}
