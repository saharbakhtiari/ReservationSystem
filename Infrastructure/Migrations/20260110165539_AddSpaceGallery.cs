using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSpaceGallery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "SpaceFile");

            migrationBuilder.AddColumn<long>(
                name: "MainImageId",
                table: "Space",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Space_MainImageId",
                table: "Space",
                column: "MainImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Space_SpaceFile_MainImageId",
                table: "Space",
                column: "MainImageId",
                principalTable: "SpaceFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Space_SpaceFile_MainImageId",
                table: "Space");

            migrationBuilder.DropIndex(
                name: "IX_Space_MainImageId",
                table: "Space");

            migrationBuilder.DropColumn(
                name: "MainImageId",
                table: "Space");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "SpaceFile",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
