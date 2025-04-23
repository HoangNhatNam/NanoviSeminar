using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NanoviConference.Migrations
{
    /// <inheritdoc />
    public partial class MergeRoomBookingIntoSession3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomBookings");

            migrationBuilder.AddColumn<int>(
                name: "ConferenceRoomRoomId",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionTime",
                table: "Sessions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Sessions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eda7d873-ae15-475d-9ecc-8ec243254529", "AQAAAAIAAYagAAAAEJxUQsTOuZfISmtJhRvpdY9ZYvcQoCYAQVhUiDhzhQHQkxASXFuo3C4Y9wulA0mLBw==", "7821743c-9340-4d0e-94b7-c0d4e43994dc" });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ConferenceRoomRoomId",
                table: "Sessions",
                column: "ConferenceRoomRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_ConferenceRooms_ConferenceRoomRoomId",
                table: "Sessions",
                column: "ConferenceRoomRoomId",
                principalTable: "ConferenceRooms",
                principalColumn: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_ConferenceRooms_ConferenceRoomRoomId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ConferenceRoomRoomId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ConferenceRoomRoomId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SessionTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sessions");

            migrationBuilder.CreateTable(
                name: "RoomBookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ConferenceRoomRoomId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "reserved")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_RoomBookings_ConferenceRooms_ConferenceRoomRoomId",
                        column: x => x.ConferenceRoomRoomId,
                        principalTable: "ConferenceRooms",
                        principalColumn: "RoomId");
                    table.ForeignKey(
                        name: "FK_RoomBookings_ConferenceRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "ConferenceRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d057bd2c-0821-403b-a486-703116234425", "AQAAAAIAAYagAAAAEDVwJ97rMA0k6Z0GWMbR1vDwT94x1oNynTjwNw9pVOTuFHNsHi7NmAkSBbjLQc4dnQ==", "bba86e0e-ec0a-411d-a974-8d028d6f23cf" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_ConferenceRoomRoomId",
                table: "RoomBookings",
                column: "ConferenceRoomRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_RoomId",
                table: "RoomBookings",
                column: "RoomId");
        }
    }
}
