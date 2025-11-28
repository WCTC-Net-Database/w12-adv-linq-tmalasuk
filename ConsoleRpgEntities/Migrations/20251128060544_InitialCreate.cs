using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConsoleRpgEntities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AbilityType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ManaCost = table.Column<int>(type: "int", nullable: false),
                    TurnUsed = table.Column<int>(type: "int", nullable: false),
                    BuffDuration = table.Column<int>(type: "int", nullable: false),
                    Stacks = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    KeyFormed = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    StoneGrabbed = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CrackFound = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    MonsterType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    StunStack = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monsters_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    SkillPoints = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    StunStack = table.Column<int>(type: "int", nullable: false),
                    ClassType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: true),
                    CurrentRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Rooms_CurrentRoomId",
                        column: x => x.CurrentRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomItems_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAbilities",
                columns: table => new
                {
                    AbilitiesId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAbilities", x => new { x.AbilitiesId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_PlayerAbilities_Abilities_AbilitiesId",
                        column: x => x.AbilitiesId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAbilities_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCategory = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: true),
                    RoomItemsId = table.Column<int>(type: "int", nullable: true),
                    BuffDuration = table.Column<int>(type: "int", nullable: true),
                    ConsumableType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurnUsed = table.Column<int>(type: "int", nullable: true),
                    EquipmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentSlot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialValue = table.Column<int>(type: "int", nullable: true),
                    SpecialDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantedAbilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Abilities_GrantedAbilityId",
                        column: x => x.GrantedAbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_RoomItems_RoomItemsId",
                        column: x => x.RoomItemsId,
                        principalTable: "RoomItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Equipped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    HeadId = table.Column<int>(type: "int", nullable: true),
                    ChestId = table.Column<int>(type: "int", nullable: true),
                    LegsId = table.Column<int>(type: "int", nullable: true),
                    FeetId = table.Column<int>(type: "int", nullable: true),
                    HandsId = table.Column<int>(type: "int", nullable: true),
                    WeaponId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipped_Items_ChestId",
                        column: x => x.ChestId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipped_Items_FeetId",
                        column: x => x.FeetId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipped_Items_HandsId",
                        column: x => x.HandsId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipped_Items_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipped_Items_LegsId",
                        column: x => x.LegsId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipped_Items_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipped_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "Id", "AbilityType", "BuffDuration", "Description", "ManaCost", "Name", "TurnUsed" },
                values: new object[] { 1, "Arcane", 0, "Blasts a foe with high magic damage based on intelligence", 20, "Arcane Barrage", 0 });

            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "Id", "AbilityType", "BuffDuration", "Description", "ManaCost", "Name", "Stacks", "TurnUsed" },
                values: new object[] { 2, "Healing", 3, "Initial heal with lingering heal over 3 turns", 12, "Nature's Embrace", 0, 0 });

            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "Id", "AbilityType", "BuffDuration", "Description", "ManaCost", "Name", "TurnUsed" },
                values: new object[,]
                {
                    { 3, "Physical", 1, "A swift attack that deals damage and has a chance to stun based on agility", 8, "Shadow Veil", 0 },
                    { 4, "Defensive", 1, "Protects against next enemy attack and deflects it back", 10, "Nullifying Aegis", 0 },
                    { 5, "Hybrid", 0, "Strike enemy and heal for damage done", 8, "Siphoning Strike", 0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "EquipmentType", "InventoryId", "ItemCategory", "Name", "Rarity", "RoomItemsId", "EquipmentSlot", "SpecialDescription", "SpecialType", "SpecialValue", "Value", "Weight" },
                values: new object[,]
                {
                    { 1, "A simple leather cap, scuffed but serviceable.", "Defense", null, "Equipment", "Worn Leather Cap", "Common", null, "Head", null, null, 0, 2, 0.5m },
                    { 2, "A thin hood offering minimal protection from blows.", "Defense", null, "Equipment", "Frayed Cloth Hood", "Common", null, "Head", null, null, 0, 1, 0.3m },
                    { 3, "A cheap metal helm often issued to rookie militia.", "Defense", null, "Equipment", "Novice Guard Helm", "Common", null, "Head", null, null, 0, 3, 1.0m },
                    { 4, "A plain cloth tunic worn by most novice adventurers.", "Defense", null, "Equipment", "Traveler's Tunic", "Common", null, "Chest", null, null, 0, 4, 1.0m },
                    { 5, "A quilted vest that provides slight impact protection.", "Defense", null, "Equipment", "Padded Vest", "Common", null, "Chest", null, null, 0, 6, 1.5m },
                    { 6, "A basic leather jerkin with uneven stitching.", "Defense", null, "Equipment", "Stitched Leather Jerkin", "Common", null, "Chest", null, null, 0, 5, 1.8m },
                    { 7, "Simple trousers made from coarse cloth.", "Defense", null, "Equipment", "Rough Cloth Trousers", "Common", null, "Legs", null, null, 0, 2, 0.9m },
                    { 8, "Light padding sewn into durable cloth makes these ideal for training.", "Defense", null, "Equipment", "Padded Leggings", "Common", null, "Legs", null, null, 0, 3, 1.2m },
                    { 9, "Sturdy breeches made from untreated leather.", "Defense", null, "Equipment", "Leather Breeches", "Common", null, "Legs", null, null, 0, 4, 1.5m },
                    { 10, "Boots stitched together from mismatched scraps of leather.", "Defense", null, "Equipment", "Patchwork Boots", "Common", null, "Feet", null, null, 0, 3, 0.8m },
                    { 11, "Soft cloth shoes with thin soles.", "Defense", null, "Equipment", "Cloth Shoes", "Common", null, "Feet", null, null, 0, 1, 0.4m },
                    { 12, "Reliable boots built for long walks on rough terrain.", "Defense", null, "Equipment", "Traveler’s Boots", "Common", null, "Feet", null, null, 0, 4, 1.0m },
                    { 13, "Stiff gloves made of untreated hide.", "Defense", null, "Equipment", "Roughhide Gloves", "Common", null, "Hands", null, null, 0, 1, 0.3m },
                    { 14, "Simple hand wraps used to prevent blistering.", "Defense", null, "Equipment", "Linen Wraps", "Common", null, "Hands", null, null, 0, 1, 0.1m },
                    { 15, "Gloves originally meant for laborers, now repurposed for adventuring.", "Defense", null, "Equipment", "Sturdy Work Gloves", "Common", null, "Hands", null, null, 0, 2, 0.4m },
                    { 16, "An old dagger with a chipped blade and patches of rust.", "Attack", null, "Equipment", "Rusty Iron Dagger", "Common", null, "Weapon", null, null, 0, 6, 0.7m },
                    { 17, "A wooden practice sword used by recruits.", "Attack", null, "Equipment", "Wooden Training Sword", "Common", null, "Weapon", null, null, 0, 6, 1.4m },
                    { 18, "A basic iron shortsword favored by town guards.", "Attack", null, "Equipment", "Iron Shortsword", "Common", null, "Weapon", null, null, 0, 6, 2.1m },
                    { 19, "A leather cap strengthened with thin metal plates.", "Defense", null, "Equipment", "Reinforced Leather Cap", "Uncommon", null, "Head", null, null, 0, 6, 0.7m },
                    { 20, "A durable hood favored by scouts and couriers.", "Defense", null, "Equipment", "Traveler’s Scout Hood", "Uncommon", null, "Head", null, null, 0, 5, 0.4m },
                    { 21, "A metal helm trimmed with bronze for added protection.", "Defense", null, "Equipment", "Bronze Rim Helm", "Uncommon", null, "Head", null, null, 0, 8, 1.3m },
                    { 22, "A light tunic with added leather padding around the torso.", "Defense", null, "Equipment", "Reinforced Tunic", "Uncommon", null, "Chest", null, null, 0, 7, 1.3m },
                    { 23, "A vest treated and hardened for improved protection.", "Defense", null, "Equipment", "Hardened Leather Vest", "Uncommon", null, "Chest", null, null, 0, 10, 2.0m },
                    { 24, "A leather jerkin reinforced with bronze studs.", "Defense", null, "Equipment", "Bronze Studded Jerkin", "Uncommon", null, "Chest", null, null, 0, 12, 2.2m },
                    { 25, "Cloth trousers fitted with extra stitching and padding.", "Defense", null, "Equipment", "Reinforced Trousers", "Uncommon", null, "Legs", null, null, 0, 5, 1.2m },
                    { 26, "Leggings strengthened with layered leather strips.", "Defense", null, "Equipment", "Leather Guard Leggings", "Uncommon", null, "Legs", null, null, 0, 7, 1.6m },
                    { 27, "Greaves with bronze plating to protect against heavier strikes.", "Defense", null, "Equipment", "Bronze-Plated Greaves", "Uncommon", null, "Legs", null, null, 0, 11, 2.4m },
                    { 28, "Sturdy boots lined with reinforced stitching.", "Defense", null, "Equipment", "Reinforced Leather Boots", "Uncommon", null, "Feet", null, null, 0, 6, 1.1m },
                    { 29, "Reliable boots with reinforced soles for long journeys.", "Defense", null, "Equipment", "Traveler’s Trail Boots", "Uncommon", null, "Feet", null, null, 0, 7, 0.9m },
                    { 30, "Boots fitted with bronze toe plates for extra durability.", "Defense", null, "Equipment", "Bronze-Toed Boots", "Uncommon", null, "Feet", null, null, 0, 10, 1.4m },
                    { 31, "Leather gloves strengthened with extra padding.", "Defense", null, "Equipment", "Reinforced Gloves", "Uncommon", null, "Hands", null, null, 0, 4, 0.4m },
                    { 32, "Cloth wraps treated with resin for improved grip.", "Defense", null, "Equipment", "Traveler’s Gripping Wraps", "Uncommon", null, "Hands", null, null, 0, 3, 0.2m },
                    { 33, "Gloves fitted with bronze plating over the knuckles.", "Defense", null, "Equipment", "Bronze-Knuckle Gloves", "Uncommon", null, "Hands", null, null, 0, 6, 0.6m },
                    { 34, "A well-maintained dagger with a sharp, polished blade.", "Attack", null, "Equipment", "Sharpened Iron Dagger", "Uncommon", null, "Weapon", null, null, 0, 7, 0.6m },
                    { 35, "A shortsword forged with care, perfectly weighted for quick strikes.", "Attack", null, "Equipment", "Balanced Shortsword", "Uncommon", null, "Weapon", null, null, 0, 10, 2.0m },
                    { 36, "A compact hatchet with a keen edge used by woodsmen and fighters alike.", "Attack", null, "Equipment", "Honed Hatchet", "Uncommon", null, "Weapon", null, null, 0, 9, 2.3m },
                    { 37, "A polished steel helm engraved with runes that sharpen awareness.", "Defense", null, "Equipment", "Helm of the Vigilant", "Rare", null, "Head", null, null, 0, 35, 1.1m },
                    { 38, "A lightweight visor forged from moonlit alloy, cool to the touch.", "Defense", null, "Equipment", "Moonsteel Visor", "Rare", null, "Head", null, null, 0, 38, 0.9m },
                    { 39, "A soft cowl that faintly rustles, granting clarity in the chaos of battle.", "Defense", null, "Equipment", "Cowl of Whispering Winds", "Rare", null, "Head", null, null, 0, 40, 0.6m },
                    { 40, "A reinforced vest dyed in deep azure, rumored to resist magical forces.", "Defense", null, "Equipment", "Azureguard Vest", "Rare", null, "Chest", null, null, 0, 55, 2.4m },
                    { 41, "Leather jerkins made from creatures that roam storm peaks, crackling faintly.", "Defense", null, "Equipment", "Stormhide Jerkin", "Rare", null, "Chest", null, null, 0, 52, 2.0m },
                    { 42, "A fine chainmail interwoven with filaments of gold, both beautiful and sturdy.", "Defense", null, "Equipment", "Gilded Chainmail", "Rare", null, "Chest", null, null, 0, 60, 3.1m },
                    { 43, "Dark leggings that grant silent steps, woven from shadowy fibers.", "Defense", null, "Equipment", "Sablestride Leggings", "Rare", null, "Legs", null, null, 0, 42, 1.6m },
                    { 44, "Greaves woven with thin strands of metal, offering surprising flexibility.", "Defense", null, "Equipment", "Ironweave Greaves", "Rare", null, "Legs", null, null, 0, 48, 2.2m },
                    { 45, "Heat-tempered trousers warm to the touch, rumored to bolster stamina.", "Defense", null, "Equipment", "Emberforged Trousers", "Rare", null, "Legs", null, null, 0, 50, 1.8m },
                    { 46, "Boots that feel impossibly light, favored by scouts and couriers.", "Defense", null, "Equipment", "Winddancer Boots", "Rare", null, "Feet", null, null, 0, 37, 1.0m },
                    { 47, "Heavy sabatons that anchor the wearer firmly to the ground.", "Defense", null, "Equipment", "Stonegrip Sabatons", "Rare", null, "Feet", null, null, 0, 45, 2.3m },
                    { 48, "Sandals carved with tiny runes that pulse softly with magic.", "Defense", null, "Equipment", "Runetread Sandals", "Rare", null, "Feet", null, null, 0, 41, 0.7m },
                    { 49, "Supple gloves used by spies; enhance precise movements.", "Defense", null, "Equipment", "Gloves of Subtlety", "Rare", null, "Hands", null, null, 0, 33, 0.5m },
                    { 50, "Thick gauntlets reinforced with condensed steel plating.", "Defense", null, "Equipment", "Hammerfist Gauntlets", "Rare", null, "Hands", null, null, 0, 47, 1.9m },
                    { 51, "Gloves touched by frost magic, cold but empowering.", "Defense", null, "Equipment", "Frostlace Mitts", "Rare", null, "Hands", null, null, 0, 44, 0.6m },
                    { 52, "A razor-sharp dagger forged from pure silver alloy.", "Attack", null, "Equipment", "Silverbrand Dagger", "Rare", null, "Weapon", null, null, 0, 55, 0.9m },
                    { 53, "A tempered longsword carried by elite knights sworn to ancient vows.", "Attack", null, "Equipment", "Oathkeeper Blade", "Rare", null, "Weapon", null, null, 0, 68, 2.4m },
                    { 54, "A wooden staff grown from enchanted trees, warm with life magic.", "Attack", null, "Equipment", "Groveheart Staff", "Rare", null, "Weapon", null, null, 0, 63, 1.8m },
                    { 55, "A radiant circlet infused with starlight, sharpening the mind and senses.", "Defense", null, "Equipment", "Crown of Starbound Insight", "Epic", null, "Head", null, null, 0, 120, 1.0m },
                    { 56, "A helm crafted from polished dragonbone, offering powerful protection.", "Defense", null, "Equipment", "Dragonbone Helm", "Epic", null, "Head", null, null, 0, 135, 1.6m },
                    { 57, "A silken hood that glows faintly, granting glimpses of future possibilities.", "Defense", null, "Equipment", "Oracle's Veiled Hood", "Epic", null, "Head", null, null, 0, 128, 0.7m },
                    { 58, "A majestic chestplate forged with astral essence, resonating with cosmic force.", "Defense", null, "Equipment", "Astralplate Aegis", "Epic", null, "Chest", null, null, 0, 210, 3.4m },
                    { 59, "A fiery garment infused with rebirth magic, warm and pulsing with life.", "Defense", null, "Equipment", "Phoenixheart Vest", "Epic", null, "Chest", null, null, 0, 195, 2.1m },
                    { 60, "Midnight-black armor worn by elite shadow sentinels.", "Defense", null, "Equipment", "Nightwarden Brigandine", "Epic", null, "Chest", null, null, 0, 205, 2.8m },
                    { 61, "Greaves shimmering with star-metal dust, enhancing agility.", "Defense", null, "Equipment", "Celestial Greaves", "Epic", null, "Legs", null, null, 0, 160, 2.2m },
                    { 62, "Heated plates forged in volcanic flame, burning with unmatched vigor.", "Defense", null, "Equipment", "Emberstride Tassets", "Epic", null, "Legs", null, null, 0, 172, 1.9m },
                    { 63, "Legguards wrapped in swirling storm energy that quickens every step.", "Defense", null, "Equipment", "Stormgait Legguards", "Epic", null, "Legs", null, null, 0, 168, 2.0m },
                    { 64, "Sabatons that defy gravity subtly, letting the wearer move like the wind.", "Defense", null, "Equipment", "Skysprint Sabatons", "Epic", null, "Feet", null, null, 0, 150, 1.4m },
                    { 65, "Boots heated from within, leaving glowing footprints in dark places.", "Defense", null, "Equipment", "Moltenmarch Boots", "Epic", null, "Feet", null, null, 0, 165, 1.7m },
                    { 66, "Boots that chill the ground beneath them, empowering steady movement.", "Defense", null, "Equipment", "Frostbound Walkers", "Epic", null, "Feet", null, null, 0, 158, 1.2m },
                    { 67, "Gauntlets humming with arcane currents that enhance striking power.", "Defense", null, "Equipment", "Gauntlets of Arcane Might", "Epic", null, "Hands", null, null, 0, 140, 0.9m },
                    { 68, "Gloves woven from spirit-thread, strengthening focus and precision.", "Defense", null, "Equipment", "Soulwoven Grips", "Epic", null, "Hands", null, null, 0, 150, 0.6m },
                    { 69, "Massive handguards etched with titan runes, heavy but immensely protective.", "Defense", null, "Equipment", "Titanforge Handguards", "Epic", null, "Hands", null, null, 0, 155, 1.5m },
                    { 70, "A dagger forged in shadow, its edge seemingly slicing through reality.", "Attack", null, "Equipment", "Voidpiercer Dagger", "Epic", null, "Weapon", null, null, 0, 180, 0.8m },
                    { 71, "A greatsword burning with solar fire, radiant even in darkness.", "Attack", null, "Equipment", "Solarbrand Greatblade", "Epic", null, "Weapon", null, null, 0, 225, 3.6m },
                    { 72, "A staff carved from an ancient living tree, pulsing with primal magic.", "Attack", null, "Equipment", "Elderroot Warstaff", "Epic", null, "Weapon", null, null, 0, 210, 2.3m },
                    { 73, "A crown imbued with the essence of kings, granting unmatched wisdom and authority.", "Defense", null, "Equipment", "Crown of Eternal Dominion", "Legendary", null, "Head", null, null, 0, 180, 1.2m },
                    { 74, "Forged from the core of a fallen star, it radiates celestial power.", "Defense", null, "Equipment", "Helm of the Fallen Star", "Legendary", null, "Head", null, null, 0, 190, 1.5m },
                    { 75, "A veil that sharpens the intellect beyond mortal limits.", "Defense", null, "Equipment", "Veil of the Infinite Mind", "Legendary", null, "Head", null, null, 0, 180, 0.9m },
                    { 76, "An armor infused with celestial energy, nearly indestructible.", "Defense", null, "Equipment", "Armor of the Celestial Sentinel", "Legendary", null, "Chest", null, null, 0, 212, 4.0m },
                    { 77, "Forged from the heart of a dragon, impervious to fire and ice.", "Defense", null, "Equipment", "Dragonheart Plate", "Legendary", null, "Chest", null, null, 0, 215, 3.8m },
                    { 78, "A mantle that burns with unending flame, empowering its wearer.", "Defense", null, "Equipment", "Mantle of the Eternal Flame", "Legendary", null, "Chest", null, null, 0, 210, 3.2m },
                    { 79, "Leg armor of a timeless guardian, offering unmatched mobility and protection.", "Defense", null, "Equipment", "Legplates of the Immortal Guardian", "Legendary", null, "Legs", null, null, 0, 215, 2.5m },
                    { 80, "Flaming greaves reborn from ashes, increasing agility and fire resistance.", "Defense", null, "Equipment", "Greaves of the Astral Phoenix", "Legendary", null, "Legs", null, null, 0, 220, 2.3m },
                    { 81, "Crafted with stormsteel, crackling with lightning energy that empowers every step.", "Defense", null, "Equipment", "Stormforged Legguards", "Legendary", null, "Legs", null, null, 0, 231, 2.4m },
                    { 82, "Boots that allow the wearer to stride across great distances with ease.", "Defense", null, "Equipment", "Boots of the Infinite Horizon", "Legendary", null, "Feet", null, null, 0, 199, 1.5m },
                    { 83, "Sabatons that grant speed and lightness, as if walking on dragon wings.", "Defense", null, "Equipment", "Dragonwing Sabatons", "Legendary", null, "Feet", null, null, 0, 203, 1.7m },
                    { 84, "Boots that leave a trail of frost, chilling enemies nearby.", "Defense", null, "Equipment", "Frostwind Walkers", "Legendary", null, "Feet", null, null, 0, 214, 1.4m },
                    { 85, "Gauntlets that grant immense strength, capable of breaking mountains.", "Defense", null, "Equipment", "Gauntlets of the Worldbreaker", "Legendary", null, "Hands", null, null, 0, 182, 1.2m },
                    { 86, "Gloves that can channel and manipulate the very essence of souls.", "Defense", null, "Equipment", "Soulgrasp Gloves", "Legendary", null, "Hands", null, null, 0, 192, 0.9m },
                    { 87, "Handguards forged from titan iron, offering unstoppable grip and defense.", "Defense", null, "Equipment", "Titanclad Handguards", "Legendary", null, "Hands", null, null, 0, 185, 1.5m },
                    { 88, "A blade whispering with ghostly voices.", "Attack", null, "Equipment", "Soulreaper Blade", "Legendary", null, "Weapon", null, null, 0, 250, 3.8m },
                    { 89, "A greatsword burning with solar energy", "Attack", null, "Equipment", "Sunforged Greatsword", "Legendary", null, "Weapon", null, null, 0, 265, 4.0m },
                    { 90, "A staff infused with ancient magic", "Attack", null, "Equipment", "Staff of the Primordial Sage", "Legendary", null, "Weapon", null, null, 0, 255, 2.8m },
                    { 91, "A crown woven from starlight.", "Defense", null, "Equipment", "Crown of the Celestial Emperor", "Mythic", null, "Head", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 500, 1.3m },
                    { 92, "A helm made with mythical steel, wings decorating each side", "Defense", null, "Equipment", "Helm of the Eternal Sentinel", "Mythic", null, "Head", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 510, 1.5m },
                    { 93, "A hood that sharpens the mind to perceive truths hidden from mortals.", "Defense", null, "Equipment", "Hood of Infinite Insight", "Mythic", null, "Head", "Grants additional dodge chance", "Shadow", 5, 520, 1.0m },
                    { 94, "A chestplate with an ornate carvings throughout", "Defense", null, "Equipment", "Armor of the Primordial Guardian", "Mythic", null, "Chest", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 600, 5.0m },
                    { 95, "This flowing robe pulses with a quiet, magical energy.", "Defense", null, "Equipment", "Robe of the Arcane Well", "Mythic", null, "Chest", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 680, 4.8m },
                    { 96, "A tunic forged of the shadows.", "Defense", null, "Equipment", "Tunic of the Silent Stalker", "Mythic", null, "Chest", "Grants additional dodge chance", "Shadow", 5, 650, 4.2m },
                    { 97, "Black steel leg armor, wrapped in mythical magics.", "Defense", null, "Equipment", "Legplates of the Timeless Guardian", "Mythic", null, "Legs", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 680, 3.0m },
                    { 98, "Leggings imbued with time magic, allowing for greater evasion.", "Defense", null, "Equipment", "Whisper of Time Leggings", "Mythic", null, "Legs", "Grants additional dodge chance", "Shadow", 5, 670, 2.8m },
                    { 99, "These flowing silk leggings shimmer faintly, humming with latent energy that replenishes the wearer’s magical reserves.", "Defense", null, "Equipment", "Moonlit Silk Legwraps", "Mythic", null, "Legs", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 695, 2.9m },
                    { 100, "Boots that allow the wearer to traverse impossible distances effortlessly.", "Defense", null, "Equipment", "Boots of the Endless Horizon", "Mythic", null, "Feet", "Grants additional dodge chance", "Shadow", 5, 650, 1.6m },
                    { 101, "Sabatons forged of mythical steel, thorns dotting its surface.", "Defense", null, "Equipment", "Dragonflight Sabatons", "Mythic", null, "Feet", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 665, 1.8m },
                    { 102, "Runes sewn into the fabric hum softly, feeding the wearer’s arcane energy continuously.", "Defense", null, "Equipment", "Starlight Treads", "Mythic", null, "Feet", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 640, 1.5m },
                    { 103, "Gauntlets forged with mythical steel, thorns dotting its surface.", "Defense", null, "Equipment", "Gauntlets of the Worldshaper", "Mythic", null, "Hands", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 680, 1.4m },
                    { 104, "Gloves that manipulate soul energy.", "Defense", null, "Equipment", "Soulgrasp Mythic Gloves", "Mythic", null, "Hands", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 690, 1.0m },
                    { 105, "Handguards forged with sleek black metal, allowing one's hands to glide effortlessly through the air.", "Defense", null, "Equipment", "Titanforge Handguards", "Mythic", null, "Hands", "Grants additional dodge chance", "Shadow", 5, 600, 1.6m },
                    { 106, "A slender dagger that guides its wielder’s movements, allowing them to evade attacks with uncanny precision..", "Attack", null, "Equipment", "Dagger of the Elusive Shadow", "Mythic", null, "Weapon", "Grants additional dodge chance", "Shadow", 5, 1200, 4.0m },
                    { 107, "A greatsword blazing with solar energy.", "Attack", null, "Equipment", "Sunfire Greatsword", "Mythic", null, "Weapon", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 1225, 4.2m },
                    { 108, "A staff imbued with raw creation magic", "Attack", null, "Equipment", "Staff of Primordial Forces", "Mythic", null, "Weapon", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 1210, 3.0m },
                    { 109, "A crown forged from the bones of fallen gods.", "Defense", null, "Equipment", "Crown of the Gods", "Ancient", null, "Head", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 5000, 2.0m },
                    { 110, "Helm that grants absolute authority over mortal and immortal alike, with unyielding protection.", "Defense", null, "Equipment", "Helm of Eternal Dominion", "Ancient", null, "Head", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 5050, 2.2m },
                    { 111, "A mask that reveals the hidden threads of fate, allowing the wearer to manipulate destiny.", "Defense", null, "Equipment", "Mask of the Voidseer", "Ancient", null, "Head", "Grants additional dodge chance", "Shadow", 20, 5100, 1.8m },
                    { 112, "A chestplate forged from the fabric of stars.", "Defense", null, "Equipment", "Armor of the Cosmos", "Ancient", null, "Chest", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 8000, 8.0m },
                    { 113, "A leather chest piece forged of ancient magic, bending time around it.", "Defense", null, "Equipment", "Dragon King’s Carapace", "Ancient", null, "Chest", "Grants additional dodge chance", "Shadow", 20, 7900, 7.8m },
                    { 114, "A robe forged in ancient world magic.", "Defense", null, "Equipment", "Robe of the World Essence", "Ancient", null, "Chest", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 7800, 7.5m },
                    { 115, "Leg armor that grants unmatched speed, strength, and stability against any foe.", "Defense", null, "Equipment", "Legguards of the Eternal Colossus", "Ancient", null, "Legs", "Grants additional dodge chance", "Shadow", 20, 4500, 5.0m },
                    { 116, "Breeches imbued with cosmic energy.", "Defense", null, "Equipment", "Astral Breeches of the Cosmos", "Ancient", null, "Legs", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 4550, 4.8m },
                    { 117, "Ancient steel legplates with myseriously etched runes dotting its surface.", "Defense", null, "Equipment", "Legplates of Ancient Steel", "Ancient", null, "Legs", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 4600, 4.9m },
                    { 118, "Boots that allow the wearer to move faster than the eye can follow, crossing worlds.", "Defense", null, "Equipment", "Boots of the Infinite Horizon", "Ancient", null, "Feet", "Grants additional dodge chance", "Shadow", 20, 4200, 2.5m },
                    { 119, "Sabatons granting etched in torns.", "Defense", null, "Equipment", "Dragonflight Winged Sabatons", "Ancient", null, "Feet", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 4250, 2.7m },
                    { 120, "Ancient slippers wrapped in magic from the gods.", "Defense", null, "Equipment", "Slippers of Eternity", "Ancient", null, "Feet", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 4300, 2.4m },
                    { 121, "Leather groves with diamond tipped claws that can redirect attacks at whim.", "Defense", null, "Equipment", "Claws of the Worldshaper", "Ancient", null, "Hands", "Grants additional dodge chance", "Shadow", 20, 4600, 2.2m },
                    { 122, "Gloves that manipulate souls and channel all forms of energy.", "Defense", null, "Equipment", "Soulgrasp Gloves of Eternity", "Ancient", null, "Hands", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 4650, 2.0m },
                    { 123, "Handguards forged from primordial titan metal, unmatched in strength and durability.", "Defense", null, "Equipment", "Titanforge Gauntlets", "Ancient", null, "Hands", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 4700, 2.5m },
                    { 124, "A dagger that devours all matter it touches, leaving only void in its wake.", "Attack", null, "Equipment", "Dagger of the Infinite Abyss", "Ancient", null, "Weapon", "Grants additional dodge chance", "Shadow", 20, 8000, 6.0m },
                    { 125, "A sword blazing with solar fury, incinerating entire armies in a single swing.", "Attack", null, "Equipment", "Sword of the Gods", "Ancient", null, "Weapon", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 8050, 6.2m },
                    { 126, "A staff that channels the power of creation itself, capable of reshaping reality.", "Attack", null, "Equipment", "Staff of Creation's End", "Ancient", null, "Weapon", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 8100, 5.5m }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BuffDuration", "ConsumableType", "Description", "InventoryId", "ItemCategory", "Name", "Rarity", "RoomItemsId", "TurnUsed", "Value", "Weight" },
                values: new object[,]
                {
                    { 301, null, "Mana", "A weak mana potion. Restores a small amount of mana instantly.", null, "Consumable", "Weak Mana Infusion", "Common", null, 0, 5, 0.1m },
                    { 302, null, "Mana", "An uncommon mana potion. Restores a moderate amount of mana instantly.", null, "Consumable", "Lesser Mana Infusion", "Uncommon", null, 0, 20, 0.2m },
                    { 303, null, "Mana", "A rare mana potion. Restores a significant amount of mana instantly.", null, "Consumable", "Mana Infusion", "Rare", null, 0, 80, 0.4m },
                    { 304, null, "Mana", "An epic mana potion. Restores a large amount of mana instantly.", null, "Consumable", "Greater Mana Infusion", "Epic", null, 0, 150, 0.6m },
                    { 305, null, "Mana", "A legendary mana potion. Restores a huge amount of mana instantly.", null, "Consumable", "Legendary Mana Infusion", "Legendary", null, 0, 350, 0.8m },
                    { 306, null, "Mana", "A mythic mana potion. Restores an extraordinary amount of mana instantly.", null, "Consumable", "Mythic Mana Infusion", "Mythic", null, 0, 600, 1.0m },
                    { 307, null, "Mana", "An ancient mana potion. Restores a legendary quantity of mana instantly.", null, "Consumable", "Ancient Mana Infusion", "Ancient", null, 0, 1000, 2.0m },
                    { 308, null, "Heal", "A weak healing potion. Restores a small amount of health instantly.", null, "Consumable", "Weak Healing Infusion", "Common", null, 0, 5, 0.1m },
                    { 309, null, "Heal", "An uncommon healing potion. Restores a moderate amount of health instantly.", null, "Consumable", "Lesser Healing Infusion", "Uncommon", null, 0, 20, 0.2m },
                    { 310, null, "Heal", "A rare healing potion. Restores a significant amount of health instantly.", null, "Consumable", "Healing Infusion", "Rare", null, 0, 80, 0.4m },
                    { 312, null, "Heal", "An epic healing potion. Restores a large amount of health instantly.", null, "Consumable", "Greater Healing Infusion", "Epic", null, 0, 150, 0.6m },
                    { 313, null, "Heal", "A legendary healing potion. Restores a huge amount of health instantly.", null, "Consumable", "Legendary Healing Infusion", "Legendary", null, 0, 350, 0.8m },
                    { 314, null, "Heal", "A mythic healing potion. Restores an extraordinary amount of health instantly.", null, "Consumable", "Mythic Healing Infusion", "Mythic", null, 0, 600, 1.0m },
                    { 315, null, "Heal", "An ancient healing potion. Restores a legendary quantity of health instantly.", null, "Consumable", "Ancient Healing Infusion", "Ancient", null, 0, 1000, 2.0m },
                    { 316, 1, "Attack", "A weak attack potion. Boosts attack power for 1 turn.", null, "Consumable", "Weak Fury Phial", "Common", null, 0, 5, 0.1m },
                    { 317, 2, "Attack", "An uncommon attack potion. Boosts attack power for 2 turns.", null, "Consumable", "Lesser Fury Phial", "Uncommon", null, 0, 20, 0.2m },
                    { 318, 3, "Attack", "A rare attack potion. Boosts attack power for 3 turns.", null, "Consumable", "Fury Phial", "Rare", null, 0, 80, 0.4m },
                    { 319, 4, "Attack", "An epic attack potion. Boosts attack power for 4 turns.", null, "Consumable", "Greater Fury Phial", "Epic", null, 0, 150, 0.6m },
                    { 320, 5, "Attack", "A legendary attack potion. Boosts attack power for 5 turns.", null, "Consumable", "Legendary Fury Phial", "Legendary", null, 0, 350, 0.8m },
                    { 321, 6, "Attack", "A mythic attack potion. Boosts attack power for 6 turns.", null, "Consumable", "Mythic Fury Phial", "Mythic", null, 0, 600, 1.0m },
                    { 322, 7, "Attack", "An ancient attack potion. Boosts attack power for 7 turns.", null, "Consumable", "Ancient Fury Phial", "Ancient", null, 0, 1000, 2.0m },
                    { 323, 1, "Defense", "A weak defense potion. Increases defense for 1 turn.", null, "Consumable", "Weak Fortitude Tonic", "Common", null, 0, 5, 0.1m },
                    { 324, 2, "Defense", "An uncommon defense potion. Increases defense for 2 turns.", null, "Consumable", "Lesser Fortitude Tonic", "Uncommon", null, 0, 20, 0.2m },
                    { 325, 3, "Defense", "A rare defense potion. Increases defense for 3 turns.", null, "Consumable", "Fortitude Tonic", "Rare", null, 0, 80, 0.4m },
                    { 326, 4, "Defense", "An epic defense potion. Increases defense for 4 turns.", null, "Consumable", "Greater Fortitude Tonic", "Epic", null, 0, 150, 0.6m },
                    { 327, 5, "Defense", "A legendary defense potion. Increases defense for 5 turns.", null, "Consumable", "Legendary Fortitude Tonic", "Legendary", null, 0, 350, 0.8m },
                    { 328, 6, "Defense", "A mythic defense potion. Increases defense for 6 turns.", null, "Consumable", "Mythic Fortitude Tonic", "Mythic", null, 0, 600, 1.0m },
                    { 329, 7, "Defense", "An ancient defense potion. Increases defense for 7 turns.", null, "Consumable", "Ancient Fortitude Tonic", "Ancient", null, 0, 1000, 2.0m }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "Difficulty", "IsLocked", "Name", "RoomType" },
                values: new object[] { 1, "A dark, damp cell with chains on the walls.", 1, true, "Dungeon", "Dungeon" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "Difficulty", "Name", "RoomType" },
                values: new object[,]
                {
                    { 2, "An ominous room filled with instruments of pain.", 2, "Torture Chamber", "Torture Chamber" },
                    { 3, "A narrow stone stairwell leading upward.", 1, "Spiral Stairwell", "Stairwell" },
                    { 4, "A well-lit room with tables, chairs, and weapons racks.", 3, "Guard Room", "Guard Area" },
                    { 5, "Rows of beds and personal lockers for the castle guards.", 2, "Barracks", "Barracks" },
                    { 6, "The kitchen’s cleaning and prep area, filled with utensils and sinks.", 1, "Scullery", "Scullery" },
                    { 7, "A room stacked with weapons, armor, and training gear.", 4, "Armory", "Armory" },
                    { 8, "A beautiful, well-kept garden leading to the castle entrance.", 1, "Castle Garden", "Garden" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "GrantedAbilityId", "InventoryId", "ItemCategory", "Name", "Rarity", "RoomItemsId", "Value", "Weight" },
                values: new object[,]
                {
                    { 201, "A blue hardcover book with gold inked pages", 1, null, "Spellbook", "Tome of the Aether", "Mythic", null, 0, 1.2m },
                    { 202, "A green tome, leaves etched into the cover", 2, null, "Spellbook", "Flora's Handbook", "Mythic", null, 0, 1.2m },
                    { 203, "A worn leather journal whose pages are as black as midnight ink", 3, null, "Spellbook", "Theif's Journal", "Mythic", null, 0, 1.2m },
                    { 204, "A heavy gray volume reinforced with iron plates", 4, null, "Spellbook", "Warrior's Codex", "Mythic", null, 0, 1.2m },
                    { 205, "A sleek crimson-bound tome that feels unnaturally cool to the touch", 5, null, "Spellbook", "Vampire's Tome", "Mythic", null, 0, 1.2m }
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Difficulty", "Health", "MonsterType", "Name", "RoomId", "StunStack" },
                values: new object[,]
                {
                    { 201, 1, 100, "Goblin", "Goblin Grunt", 1, 0 },
                    { 202, 1, 100, "Goblin", "Goblin Sneak", 1, 0 },
                    { 203, 2, 200, "Goblin", "Goblin Cutthroat", 2, 0 },
                    { 204, 2, 200, "Goblin", "Goblin Scout", 2, 0 },
                    { 205, 3, 300, "Goblin", "Goblin Bruiser", 3, 0 },
                    { 206, 3, 300, "Goblin", "Goblin Raider", 3, 0 },
                    { 207, 2, 200, "Goblin", "Goblin Slinger", 2, 0 },
                    { 208, 4, 400, "Goblin", "Goblin Firestarter", 4, 0 },
                    { 209, 4, 400, "Goblin", "Goblin Shadowblade", 4, 0 },
                    { 210, 5, 500, "Goblin", "Goblin Berserker", 5, 0 },
                    { 211, 5, 500, "Goblin", "Goblin Bonecrusher", 5, 0 },
                    { 212, 3, 300, "Goblin", "Goblin Poisonblade", 3, 0 },
                    { 213, 4, 400, "Goblin", "Goblin Stormcaller", 4, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipped_ChestId",
                table: "Equipped",
                column: "ChestId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipped_FeetId",
                table: "Equipped",
                column: "FeetId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipped_HandsId",
                table: "Equipped",
                column: "HandsId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipped_HeadId",
                table: "Equipped",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipped_LegsId",
                table: "Equipped",
                column: "LegsId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipped_PlayerId",
                table: "Equipped",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipped_WeaponId",
                table: "Equipped",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PlayerId",
                table: "Inventory",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_GrantedAbilityId",
                table: "Items",
                column: "GrantedAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_InventoryId",
                table: "Items",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_RoomItemsId",
                table: "Items",
                column: "RoomItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_RoomId",
                table: "Monsters",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAbilities_PlayersId",
                table: "PlayerAbilities",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentRoomId",
                table: "Players",
                column: "CurrentRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomItems_RoomId",
                table: "RoomItems",
                column: "RoomId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipped");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "PlayerAbilities");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "RoomItems");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
