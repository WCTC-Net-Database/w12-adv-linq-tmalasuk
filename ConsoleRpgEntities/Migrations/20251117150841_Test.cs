using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleRpgEntities.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Inventory_InventoryId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_InventoryId",
                table: "Players");


            migrationBuilder.AlterColumn<string>(
                name: "MonsterType",
                table: "Monsters",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");


            migrationBuilder.AlterColumn<string>(
                name: "ItemCategory",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EquipmentSlot",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);


         

            migrationBuilder.AlterColumn<string>(
                name: "AbilityType",
                table: "Abilities",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PlayerId",
                table: "Inventory",
                column: "PlayerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Players_PlayerId",
                table: "Inventory",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Players_PlayerId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_PlayerId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "BuffDuration",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ConsumableType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EquipmentType",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemCategory",
                table: "Items",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "EquipmentSlot",
                table: "Items",
                newName: "Slot");

            migrationBuilder.AlterColumn<string>(
                name: "MonsterType",
                table: "Monsters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Slot",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AbilityType",
                table: "Abilities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.CreateIndex(
                name: "IX_Players_InventoryId",
                table: "Players",
                column: "InventoryId",
                unique: true,
                filter: "[InventoryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Inventory_InventoryId",
                table: "Players",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id");
        }
    }
}
