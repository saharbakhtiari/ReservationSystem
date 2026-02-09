using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MergeBokkingAndBookingHoldDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_TimeSlot_TimeSlotId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_TimeSlotId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "Booking");

            migrationBuilder.AddColumn<long>(
                name: "BookingId",
                table: "BookingHoldDetail",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BookingHoldId",
                table: "Booking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "BookingDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSlotId = table.Column<long>(type: "bigint", nullable: true),
                    BookingId = table.Column<long>(type: "bigint", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDetail_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingDetail_TimeSlot_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingHoldDetail_BookingId",
                table: "BookingHoldDetail",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_BookingId",
                table: "BookingDetail",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_TimeSlotId",
                table: "BookingDetail",
                column: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHoldDetail_Booking_BookingId",
                table: "BookingHoldDetail",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingHoldDetail_Booking_BookingId",
                table: "BookingHoldDetail");

            migrationBuilder.DropTable(
                name: "BookingDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookingHoldDetail_BookingId",
                table: "BookingHoldDetail");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookingHoldDetail");

            migrationBuilder.DropColumn(
                name: "BookingHoldId",
                table: "Booking");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "TimeSlotId",
                table: "Booking",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TimeSlotId",
                table: "Booking",
                column: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_TimeSlot_TimeSlotId",
                table: "Booking",
                column: "TimeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "Id");
        }
    }
}
