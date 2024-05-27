using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caber.Migrations
{
    /// <inheritdoc />
    public partial class isverifiedattributedriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "drivers");
        }
    }
}
