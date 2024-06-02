using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caber.Migrations
{
    /// <inheritdoc />
    public partial class driverattr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverStatus",
                table: "drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRide",
                table: "drivers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TotalEarnings",
                table: "drivers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverStatus",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "LastRide",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "TotalEarnings",
                table: "drivers");
        }
    }
}
