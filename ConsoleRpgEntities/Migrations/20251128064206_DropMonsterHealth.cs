using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleRpgEntities.Migrations
{
    /// <inheritdoc />
    public partial class DropMonsterHealth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Health",
                table: "Monsters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Health",
                table: "Monsters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 201,
                column: "Health",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 202,
                column: "Health",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 203,
                column: "Health",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 204,
                column: "Health",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 205,
                column: "Health",
                value: 300);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 206,
                column: "Health",
                value: 300);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 207,
                column: "Health",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 208,
                column: "Health",
                value: 400);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 209,
                column: "Health",
                value: 400);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 210,
                column: "Health",
                value: 500);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 211,
                column: "Health",
                value: 500);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 212,
                column: "Health",
                value: 300);

            migrationBuilder.UpdateData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: 213,
                column: "Health",
                value: 400);
        }
    }
}
