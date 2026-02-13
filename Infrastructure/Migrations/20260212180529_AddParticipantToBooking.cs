using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BookingId",
                table: "BookingParticipant",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingParticipant_BookingId",
                table: "BookingParticipant",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingParticipant_Booking_BookingId",
                table: "BookingParticipant",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingParticipant_Booking_BookingId",
                table: "BookingParticipant");

            migrationBuilder.DropIndex(
                name: "IX_BookingParticipant_BookingId",
                table: "BookingParticipant");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookingParticipant");
        }
    }
}
