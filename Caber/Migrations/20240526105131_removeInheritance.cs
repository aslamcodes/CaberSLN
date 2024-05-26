using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caber.Migrations
{
    /// <inheritdoc />
    public partial class removeInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_Users_Id",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_Users_Id",
                table: "passengers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "passengers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "passengers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "drivers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_passengers_UserId",
                table: "passengers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_drivers_UserId",
                table: "drivers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_Users_UserId",
                table: "drivers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_passengers_Users_UserId",
                table: "passengers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_Users_UserId",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_Users_UserId",
                table: "passengers");

            migrationBuilder.DropIndex(
                name: "IX_passengers_UserId",
                table: "passengers");

            migrationBuilder.DropIndex(
                name: "IX_drivers_UserId",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "passengers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "drivers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "passengers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "drivers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_Users_Id",
                table: "drivers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_passengers_Users_Id",
                table: "passengers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
