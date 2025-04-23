using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NanoviConference.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerGroupManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Groups_GroupId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_GroupId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SeatLine",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SeatRow",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "CustomerGroups",
                columns: table => new
                {
                    CustomerGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroups", x => x.CustomerGroupId);
                    table.ForeignKey(
                        name: "FK_CustomerGroups_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94c1260e-47f6-4b45-810b-e81a9ecf423d", "AQAAAAIAAYagAAAAEC6hahAmdNqleKb3UL1oRgTq/8/rOwP1wTMHC2auTsPsohRsMs1NU87nv+GO5hTBpw==", "3efa0171-3e4d-45a7-86f0-9522ae6924b0" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_CustomerId",
                table: "CustomerGroups",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_GroupId",
                table: "CustomerGroups",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerGroups");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatLine",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatRow",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eda7d873-ae15-475d-9ecc-8ec243254529", "AQAAAAIAAYagAAAAEJxUQsTOuZfISmtJhRvpdY9ZYvcQoCYAQVhUiDhzhQHQkxASXFuo3C4Y9wulA0mLBw==", "7821743c-9340-4d0e-94b7-c0d4e43994dc" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: "NA001",
                columns: new[] { "GroupId", "SeatLine", "SeatRow" },
                values: new object[] { 2, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: "NA002",
                columns: new[] { "GroupId", "SeatLine", "SeatRow" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GroupId",
                table: "Customers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Groups_GroupId",
                table: "Customers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
