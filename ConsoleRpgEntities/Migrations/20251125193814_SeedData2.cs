using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConsoleRpgEntities.Migrations
{
    /// <inheritdoc />
    public partial class SeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Id", "Description", "EquipmentType", "InventoryId", "ItemCategory", "MonsterId", "Name", "Rarity", "EquipmentSlot", "SpecialDescription", "SpecialType", "SpecialValue", "Value", "Weight" },
                values: new object[,]
                {
                    { 1, "A simple leather cap, scuffed but serviceable.", "Defense", null, "Equipment", null, "Worn Leather Cap", "Common", "Head", null, null, 0, 2, 0.5m },
                    { 2, "A thin hood offering minimal protection from blows.", "Defense", null, "Equipment", null, "Frayed Cloth Hood", "Common", "Head", null, null, 0, 1, 0.3m },
                    { 3, "A cheap metal helm often issued to rookie militia.", "Defense", null, "Equipment", null, "Novice Guard Helm", "Common", "Head", null, null, 0, 3, 1.0m },
                    { 4, "A plain cloth tunic worn by most novice adventurers.", "Defense", null, "Equipment", null, "Traveler's Tunic", "Common", "Chest", null, null, 0, 4, 1.0m },
                    { 5, "A quilted vest that provides slight impact protection.", "Defense", null, "Equipment", null, "Padded Vest", "Common", "Chest", null, null, 0, 6, 1.5m },
                    { 6, "A basic leather jerkin with uneven stitching.", "Defense", null, "Equipment", null, "Stitched Leather Jerkin", "Common", "Chest", null, null, 0, 5, 1.8m },
                    { 7, "Simple trousers made from coarse cloth.", "Defense", null, "Equipment", null, "Rough Cloth Trousers", "Common", "Legs", null, null, 0, 2, 0.9m },
                    { 8, "Light padding sewn into durable cloth makes these ideal for training.", "Defense", null, "Equipment", null, "Padded Leggings", "Common", "Legs", null, null, 0, 3, 1.2m },
                    { 9, "Sturdy breeches made from untreated leather.", "Defense", null, "Equipment", null, "Leather Breeches", "Common", "Legs", null, null, 0, 4, 1.5m },
                    { 10, "Boots stitched together from mismatched scraps of leather.", "Defense", null, "Equipment", null, "Patchwork Boots", "Common", "Feet", null, null, 0, 3, 0.8m },
                    { 11, "Soft cloth shoes with thin soles.", "Defense", null, "Equipment", null, "Cloth Shoes", "Common", "Feet", null, null, 0, 1, 0.4m },
                    { 12, "Reliable boots built for long walks on rough terrain.", "Defense", null, "Equipment", null, "Traveler’s Boots", "Common", "Feet", null, null, 0, 4, 1.0m },
                    { 13, "Stiff gloves made of untreated hide.", "Defense", null, "Equipment", null, "Roughhide Gloves", "Common", "Hands", null, null, 0, 1, 0.3m },
                    { 14, "Simple hand wraps used to prevent blistering.", "Defense", null, "Equipment", null, "Linen Wraps", "Common", "Hands", null, null, 0, 1, 0.1m },
                    { 15, "Gloves originally meant for laborers, now repurposed for adventuring.", "Defense", null, "Equipment", null, "Sturdy Work Gloves", "Common", "Hands", null, null, 0, 2, 0.4m },
                    { 16, "An old dagger with a chipped blade and patches of rust.", "Attack", null, "Equipment", null, "Rusty Iron Dagger", "Common", "Weapon", null, null, 0, 6, 0.7m },
                    { 17, "A wooden practice sword used by recruits.", "Attack", null, "Equipment", null, "Wooden Training Sword", "Common", "Weapon", null, null, 0, 6, 1.4m },
                    { 18, "A basic iron shortsword favored by town guards.", "Attack", null, "Equipment", null, "Iron Shortsword", "Common", "Weapon", null, null, 0, 6, 2.1m },
                    { 19, "A leather cap strengthened with thin metal plates.", "Defense", null, "Equipment", null, "Reinforced Leather Cap", "Uncommon", "Head", null, null, 0, 6, 0.7m },
                    { 20, "A durable hood favored by scouts and couriers.", "Defense", null, "Equipment", null, "Traveler’s Scout Hood", "Uncommon", "Head", null, null, 0, 5, 0.4m },
                    { 21, "A metal helm trimmed with bronze for added protection.", "Defense", null, "Equipment", null, "Bronze Rim Helm", "Uncommon", "Head", null, null, 0, 8, 1.3m },
                    { 22, "A light tunic with added leather padding around the torso.", "Defense", null, "Equipment", null, "Reinforced Tunic", "Uncommon", "Chest", null, null, 0, 7, 1.3m },
                    { 23, "A vest treated and hardened for improved protection.", "Defense", null, "Equipment", null, "Hardened Leather Vest", "Uncommon", "Chest", null, null, 0, 10, 2.0m },
                    { 24, "A leather jerkin reinforced with bronze studs.", "Defense", null, "Equipment", null, "Bronze Studded Jerkin", "Uncommon", "Chest", null, null, 0, 12, 2.2m },
                    { 25, "Cloth trousers fitted with extra stitching and padding.", "Defense", null, "Equipment", null, "Reinforced Trousers", "Uncommon", "Legs", null, null, 0, 5, 1.2m },
                    { 26, "Leggings strengthened with layered leather strips.", "Defense", null, "Equipment", null, "Leather Guard Leggings", "Uncommon", "Legs", null, null, 0, 7, 1.6m },
                    { 27, "Greaves with bronze plating to protect against heavier strikes.", "Defense", null, "Equipment", null, "Bronze-Plated Greaves", "Uncommon", "Legs", null, null, 0, 11, 2.4m },
                    { 28, "Sturdy boots lined with reinforced stitching.", "Defense", null, "Equipment", null, "Reinforced Leather Boots", "Uncommon", "Feet", null, null, 0, 6, 1.1m },
                    { 29, "Reliable boots with reinforced soles for long journeys.", "Defense", null, "Equipment", null, "Traveler’s Trail Boots", "Uncommon", "Feet", null, null, 0, 7, 0.9m },
                    { 30, "Boots fitted with bronze toe plates for extra durability.", "Defense", null, "Equipment", null, "Bronze-Toed Boots", "Uncommon", "Feet", null, null, 0, 10, 1.4m },
                    { 31, "Leather gloves strengthened with extra padding.", "Defense", null, "Equipment", null, "Reinforced Gloves", "Uncommon", "Hands", null, null, 0, 4, 0.4m },
                    { 32, "Cloth wraps treated with resin for improved grip.", "Defense", null, "Equipment", null, "Traveler’s Gripping Wraps", "Uncommon", "Hands", null, null, 0, 3, 0.2m },
                    { 33, "Gloves fitted with bronze plating over the knuckles.", "Defense", null, "Equipment", null, "Bronze-Knuckle Gloves", "Uncommon", "Hands", null, null, 0, 6, 0.6m },
                    { 34, "A well-maintained dagger with a sharp, polished blade.", "Attack", null, "Equipment", null, "Sharpened Iron Dagger", "Uncommon", "Weapon", null, null, 0, 7, 0.6m },
                    { 35, "A shortsword forged with care, perfectly weighted for quick strikes.", "Attack", null, "Equipment", null, "Balanced Shortsword", "Uncommon", "Weapon", null, null, 0, 10, 2.0m },
                    { 36, "A compact hatchet with a keen edge used by woodsmen and fighters alike.", "Attack", null, "Equipment", null, "Honed Hatchet", "Uncommon", "Weapon", null, null, 0, 9, 2.3m },
                    { 37, "A polished steel helm engraved with runes that sharpen awareness.", "Defense", null, "Equipment", null, "Helm of the Vigilant", "Rare", "Head", null, null, 0, 35, 1.1m },
                    { 38, "A lightweight visor forged from moonlit alloy, cool to the touch.", "Defense", null, "Equipment", null, "Moonsteel Visor", "Rare", "Head", null, null, 0, 38, 0.9m },
                    { 39, "A soft cowl that faintly rustles, granting clarity in the chaos of battle.", "Defense", null, "Equipment", null, "Cowl of Whispering Winds", "Rare", "Head", null, null, 0, 40, 0.6m },
                    { 40, "A reinforced vest dyed in deep azure, rumored to resist magical forces.", "Defense", null, "Equipment", null, "Azureguard Vest", "Rare", "Chest", null, null, 0, 55, 2.4m },
                    { 41, "Leather jerkins made from creatures that roam storm peaks, crackling faintly.", "Defense", null, "Equipment", null, "Stormhide Jerkin", "Rare", "Chest", null, null, 0, 52, 2.0m },
                    { 42, "A fine chainmail interwoven with filaments of gold, both beautiful and sturdy.", "Defense", null, "Equipment", null, "Gilded Chainmail", "Rare", "Chest", null, null, 0, 60, 3.1m },
                    { 43, "Dark leggings that grant silent steps, woven from shadowy fibers.", "Defense", null, "Equipment", null, "Sablestride Leggings", "Rare", "Legs", null, null, 0, 42, 1.6m },
                    { 44, "Greaves woven with thin strands of metal, offering surprising flexibility.", "Defense", null, "Equipment", null, "Ironweave Greaves", "Rare", "Legs", null, null, 0, 48, 2.2m },
                    { 45, "Heat-tempered trousers warm to the touch, rumored to bolster stamina.", "Defense", null, "Equipment", null, "Emberforged Trousers", "Rare", "Legs", null, null, 0, 50, 1.8m },
                    { 46, "Boots that feel impossibly light, favored by scouts and couriers.", "Defense", null, "Equipment", null, "Winddancer Boots", "Rare", "Feet", null, null, 0, 37, 1.0m },
                    { 47, "Heavy sabatons that anchor the wearer firmly to the ground.", "Defense", null, "Equipment", null, "Stonegrip Sabatons", "Rare", "Feet", null, null, 0, 45, 2.3m },
                    { 48, "Sandals carved with tiny runes that pulse softly with magic.", "Defense", null, "Equipment", null, "Runetread Sandals", "Rare", "Feet", null, null, 0, 41, 0.7m },
                    { 49, "Supple gloves used by spies; enhance precise movements.", "Defense", null, "Equipment", null, "Gloves of Subtlety", "Rare", "Hands", null, null, 0, 33, 0.5m },
                    { 50, "Thick gauntlets reinforced with condensed steel plating.", "Defense", null, "Equipment", null, "Hammerfist Gauntlets", "Rare", "Hands", null, null, 0, 47, 1.9m },
                    { 51, "Gloves touched by frost magic, cold but empowering.", "Defense", null, "Equipment", null, "Frostlace Mitts", "Rare", "Hands", null, null, 0, 44, 0.6m },
                    { 52, "A razor-sharp dagger forged from pure silver alloy.", "Attack", null, "Equipment", null, "Silverbrand Dagger", "Rare", "Weapon", null, null, 0, 55, 0.9m },
                    { 53, "A tempered longsword carried by elite knights sworn to ancient vows.", "Attack", null, "Equipment", null, "Oathkeeper Blade", "Rare", "Weapon", null, null, 0, 68, 2.4m },
                    { 54, "A wooden staff grown from enchanted trees, warm with life magic.", "Attack", null, "Equipment", null, "Groveheart Staff", "Rare", "Weapon", null, null, 0, 63, 1.8m },
                    { 55, "A radiant circlet infused with starlight, sharpening the mind and senses.", "Defense", null, "Equipment", null, "Crown of Starbound Insight", "Epic", "Head", null, null, 0, 120, 1.0m },
                    { 56, "A helm crafted from polished dragonbone, offering powerful protection.", "Defense", null, "Equipment", null, "Dragonbone Helm", "Epic", "Head", null, null, 0, 135, 1.6m },
                    { 57, "A silken hood that glows faintly, granting glimpses of future possibilities.", "Defense", null, "Equipment", null, "Oracle's Veiled Hood", "Epic", "Head", null, null, 0, 128, 0.7m },
                    { 58, "A majestic chestplate forged with astral essence, resonating with cosmic force.", "Defense", null, "Equipment", null, "Astralplate Aegis", "Epic", "Chest", null, null, 0, 210, 3.4m },
                    { 59, "A fiery garment infused with rebirth magic, warm and pulsing with life.", "Defense", null, "Equipment", null, "Phoenixheart Vest", "Epic", "Chest", null, null, 0, 195, 2.1m },
                    { 60, "Midnight-black armor worn by elite shadow sentinels.", "Defense", null, "Equipment", null, "Nightwarden Brigandine", "Epic", "Chest", null, null, 0, 205, 2.8m },
                    { 61, "Greaves shimmering with star-metal dust, enhancing agility.", "Defense", null, "Equipment", null, "Celestial Greaves", "Epic", "Legs", null, null, 0, 160, 2.2m },
                    { 62, "Heated plates forged in volcanic flame, burning with unmatched vigor.", "Defense", null, "Equipment", null, "Emberstride Tassets", "Epic", "Legs", null, null, 0, 172, 1.9m },
                    { 63, "Legguards wrapped in swirling storm energy that quickens every step.", "Defense", null, "Equipment", null, "Stormgait Legguards", "Epic", "Legs", null, null, 0, 168, 2.0m },
                    { 64, "Sabatons that defy gravity subtly, letting the wearer move like the wind.", "Defense", null, "Equipment", null, "Skysprint Sabatons", "Epic", "Feet", null, null, 0, 150, 1.4m },
                    { 65, "Boots heated from within, leaving glowing footprints in dark places.", "Defense", null, "Equipment", null, "Moltenmarch Boots", "Epic", "Feet", null, null, 0, 165, 1.7m },
                    { 66, "Boots that chill the ground beneath them, empowering steady movement.", "Defense", null, "Equipment", null, "Frostbound Walkers", "Epic", "Feet", null, null, 0, 158, 1.2m },
                    { 67, "Gauntlets humming with arcane currents that enhance striking power.", "Defense", null, "Equipment", null, "Gauntlets of Arcane Might", "Epic", "Hands", null, null, 0, 140, 0.9m },
                    { 68, "Gloves woven from spirit-thread, strengthening focus and precision.", "Defense", null, "Equipment", null, "Soulwoven Grips", "Epic", "Hands", null, null, 0, 150, 0.6m },
                    { 69, "Massive handguards etched with titan runes, heavy but immensely protective.", "Defense", null, "Equipment", null, "Titanforge Handguards", "Epic", "Hands", null, null, 0, 155, 1.5m },
                    { 70, "A dagger forged in shadow, its edge seemingly slicing through reality.", "Attack", null, "Equipment", null, "Voidpiercer Dagger", "Epic", "Weapon", null, null, 0, 180, 0.8m },
                    { 71, "A greatsword burning with solar fire, radiant even in darkness.", "Attack", null, "Equipment", null, "Solarbrand Greatblade", "Epic", "Weapon", null, null, 0, 225, 3.6m },
                    { 72, "A staff carved from an ancient living tree, pulsing with primal magic.", "Attack", null, "Equipment", null, "Elderroot Warstaff", "Epic", "Weapon", null, null, 0, 210, 2.3m },
                    { 73, "A crown imbued with the essence of kings, granting unmatched wisdom and authority.", "Defense", null, "Equipment", null, "Crown of Eternal Dominion", "Legendary", "Head", null, null, 0, 180, 1.2m },
                    { 74, "Forged from the core of a fallen star, it radiates celestial power.", "Defense", null, "Equipment", null, "Helm of the Fallen Star", "Legendary", "Head", null, null, 0, 190, 1.5m },
                    { 75, "A veil that sharpens the intellect beyond mortal limits.", "Defense", null, "Equipment", null, "Veil of the Infinite Mind", "Legendary", "Head", null, null, 0, 180, 0.9m },
                    { 76, "An armor infused with celestial energy, nearly indestructible.", "Defense", null, "Equipment", null, "Armor of the Celestial Sentinel", "Legendary", "Chest", null, null, 0, 212, 4.0m },
                    { 77, "Forged from the heart of a dragon, impervious to fire and ice.", "Defense", null, "Equipment", null, "Dragonheart Plate", "Legendary", "Chest", null, null, 0, 215, 3.8m },
                    { 78, "A mantle that burns with unending flame, empowering its wearer.", "Defense", null, "Equipment", null, "Mantle of the Eternal Flame", "Legendary", "Chest", null, null, 0, 210, 3.2m },
                    { 79, "Leg armor of a timeless guardian, offering unmatched mobility and protection.", "Defense", null, "Equipment", null, "Legplates of the Immortal Guardian", "Legendary", "Legs", null, null, 0, 215, 2.5m },
                    { 80, "Flaming greaves reborn from ashes, increasing agility and fire resistance.", "Defense", null, "Equipment", null, "Greaves of the Astral Phoenix", "Legendary", "Legs", null, null, 0, 220, 2.3m },
                    { 81, "Crafted with stormsteel, crackling with lightning energy that empowers every step.", "Defense", null, "Equipment", null, "Stormforged Legguards", "Legendary", "Legs", null, null, 0, 231, 2.4m },
                    { 82, "Boots that allow the wearer to stride across great distances with ease.", "Defense", null, "Equipment", null, "Boots of the Infinite Horizon", "Legendary", "Feet", null, null, 0, 199, 1.5m },
                    { 83, "Sabatons that grant speed and lightness, as if walking on dragon wings.", "Defense", null, "Equipment", null, "Dragonwing Sabatons", "Legendary", "Feet", null, null, 0, 203, 1.7m },
                    { 84, "Boots that leave a trail of frost, chilling enemies nearby.", "Defense", null, "Equipment", null, "Frostwind Walkers", "Legendary", "Feet", null, null, 0, 214, 1.4m },
                    { 85, "Gauntlets that grant immense strength, capable of breaking mountains.", "Defense", null, "Equipment", null, "Gauntlets of the Worldbreaker", "Legendary", "Hands", null, null, 0, 182, 1.2m },
                    { 86, "Gloves that can channel and manipulate the very essence of souls.", "Defense", null, "Equipment", null, "Soulgrasp Gloves", "Legendary", "Hands", null, null, 0, 192, 0.9m },
                    { 87, "Handguards forged from titan iron, offering unstoppable grip and defense.", "Defense", null, "Equipment", null, "Titanclad Handguards", "Legendary", "Hands", null, null, 0, 185, 1.5m },
                    { 88, "A blade whispering with ghostly voices.", "Attack", null, "Equipment", null, "Soulreaper Blade", "Legendary", "Weapon", null, null, 0, 250, 3.8m },
                    { 89, "A greatsword burning with solar energy", "Attack", null, "Equipment", null, "Sunforged Greatsword", "Legendary", "Weapon", null, null, 0, 265, 4.0m },
                    { 90, "A staff infused with ancient magic", "Attack", null, "Equipment", null, "Staff of the Primordial Sage", "Legendary", "Weapon", null, null, 0, 255, 2.8m },
                    { 91, "A crown woven from starlight.", "Defense", null, "Equipment", null, "Crown of the Celestial Emperor", "Mythic", "Head", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 500, 1.3m },
                    { 92, "A helm made with mythical steel, wings decorating each side", "Defense", null, "Equipment", null, "Helm of the Eternal Sentinel", "Mythic", "Head", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 510, 1.5m },
                    { 93, "A hood that sharpens the mind to perceive truths hidden from mortals.", "Defense", null, "Equipment", null, "Hood of Infinite Insight", "Mythic", "Head", "Grants additional dodge chance", "Shadow", 5, 520, 1.0m },
                    { 94, "A chestplate with an ornate carvings throughout", "Defense", null, "Equipment", null, "Armor of the Primordial Guardian", "Mythic", "Chest", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 600, 5.0m },
                    { 95, "This flowing robe pulses with a quiet, magical energy.", "Defense", null, "Equipment", null, "Robe of the Arcane Well", "Mythic", "Chest", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 680, 4.8m },
                    { 96, "A tunic forged of the shadows.", "Defense", null, "Equipment", null, "Tunic of the Silent Stalker", "Mythic", "Chest", "Grants additional dodge chance", "Shadow", 5, 650, 4.2m },
                    { 97, "Black steel leg armor, wrapped in mythical magics.", "Defense", null, "Equipment", null, "Legplates of the Timeless Guardian", "Mythic", "Legs", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 680, 3.0m },
                    { 98, "Leggings imbued with time magic, allowing for greater evasion.", "Defense", null, "Equipment", null, "Whisper of Time Leggings", "Mythic", "Legs", "Grants additional dodge chance", "Shadow", 5, 670, 2.8m },
                    { 99, "These flowing silk leggings shimmer faintly, humming with latent energy that replenishes the wearer’s magical reserves.", "Defense", null, "Equipment", null, "Moonlit Silk Legwraps", "Mythic", "Legs", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 695, 2.9m },
                    { 100, "Boots that allow the wearer to traverse impossible distances effortlessly.", "Defense", null, "Equipment", null, "Boots of the Endless Horizon", "Mythic", "Feet", "Grants additional dodge chance", "Shadow", 5, 650, 1.6m },
                    { 101, "Sabatons forged of mythical steel, thorns dotting its surface.", "Defense", null, "Equipment", null, "Dragonflight Sabatons", "Mythic", "Feet", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 665, 1.8m },
                    { 102, "Runes sewn into the fabric hum softly, feeding the wearer’s arcane energy continuously.", "Defense", null, "Equipment", null, "Starlight Treads", "Mythic", "Feet", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 640, 1.5m },
                    { 103, "Gauntlets forged with mythical steel, thorns dotting its surface.", "Defense", null, "Equipment", null, "Gauntlets of the Worldshaper", "Mythic", "Hands", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 680, 1.4m },
                    { 104, "Gloves that manipulate soul energy.", "Defense", null, "Equipment", null, "Soulgrasp Mythic Gloves", "Mythic", "Hands", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 690, 1.0m },
                    { 105, "Handguards forged with sleek black metal, allowing one's hands to glide effortlessly through the air.", "Defense", null, "Equipment", null, "Titanforge Handguards", "Mythic", "Hands", "Grants additional dodge chance", "Shadow", 5, 600, 1.6m },
                    { 106, "A slender dagger that guides its wielder’s movements, allowing them to evade attacks with uncanny precision..", "Attack", null, "Equipment", null, "Dagger of the Elusive Shadow", "Mythic", "Weapon", "Grants additional dodge chance", "Shadow", 5, 1200, 4.0m },
                    { 107, "A greatsword blazing with solar energy.", "Attack", null, "Equipment", null, "Sunfire Greatsword", "Mythic", "Weapon", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 5, 1225, 4.2m },
                    { 108, "A staff imbued with raw creation magic", "Attack", null, "Equipment", null, "Staff of Primordial Forces", "Mythic", "Weapon", "Grants additional mana regeneration per turn", "Rejuvenation", 5, 1210, 3.0m },
                    { 109, "A crown forged from the bones of fallen gods.", "Defense", null, "Equipment", null, "Crown of the Gods", "Ancient", "Head", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 5000, 2.0m },
                    { 110, "Helm that grants absolute authority over mortal and immortal alike, with unyielding protection.", "Defense", null, "Equipment", null, "Helm of Eternal Dominion", "Ancient", "Head", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 5050, 2.2m },
                    { 111, "A mask that reveals the hidden threads of fate, allowing the wearer to manipulate destiny.", "Defense", null, "Equipment", null, "Mask of the Voidseer", "Ancient", "Head", "Grants additional dodge chance", "Shadow", 20, 5100, 1.8m },
                    { 112, "A chestplate forged from the fabric of stars.", "Defense", null, "Equipment", null, "Armor of the Cosmos", "Ancient", "Chest", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 8000, 8.0m },
                    { 113, "A leather chest piece forged of ancient magic, bending time around it.", "Defense", null, "Equipment", null, "Dragon King’s Carapace", "Ancient", "Chest", "Grants additional dodge chance", "Shadow", 20, 7900, 7.8m },
                    { 114, "A robe forged in ancient world magic.", "Defense", null, "Equipment", null, "Robe of the World Essence", "Ancient", "Chest", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 7800, 7.5m },
                    { 115, "Leg armor that grants unmatched speed, strength, and stability against any foe.", "Defense", null, "Equipment", null, "Legguards of the Eternal Colossus", "Ancient", "Legs", "Grants additional dodge chance", "Shadow", 20, 4500, 5.0m },
                    { 116, "Breeches imbued with cosmic energy.", "Defense", null, "Equipment", null, "Astral Breeches of the Cosmos", "Ancient", "Legs", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 4550, 4.8m },
                    { 117, "Ancient steel legplates with myseriously etched runes dotting its surface.", "Defense", null, "Equipment", null, "Legplates of Ancient Steel", "Ancient", "Legs", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 4600, 4.9m },
                    { 118, "Boots that allow the wearer to move faster than the eye can follow, crossing worlds.", "Defense", null, "Equipment", null, "Boots of the Infinite Horizon", "Ancient", "Feet", "Grants additional dodge chance", "Shadow", 20, 4200, 2.5m },
                    { 119, "Sabatons granting etched in torns.", "Defense", null, "Equipment", null, "Dragonflight Winged Sabatons", "Ancient", "Feet", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 4250, 2.7m },
                    { 120, "Ancient slippers wrapped in magic from the gods.", "Defense", null, "Equipment", null, "Slippers of Eternity", "Ancient", "Feet", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 4300, 2.4m },
                    { 121, "Leather groves with diamond tipped claws that can redirect attacks at whim.", "Defense", null, "Equipment", null, "Claws of the Worldshaper", "Ancient", "Hands", "Grants additional dodge chance", "Shadow", 20, 4600, 2.2m },
                    { 122, "Gloves that manipulate souls and channel all forms of energy.", "Defense", null, "Equipment", null, "Soulgrasp Gloves of Eternity", "Ancient", "Hands", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 4650, 2.0m },
                    { 123, "Handguards forged from primordial titan metal, unmatched in strength and durability.", "Defense", null, "Equipment", null, "Titanforge Gauntlets", "Ancient", "Hands", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 4700, 2.5m },
                    { 124, "A dagger that devours all matter it touches, leaving only void in its wake.", "Attack", null, "Equipment", null, "Dagger of the Infinite Abyss", "Ancient", "Weapon", "Grants additional dodge chance", "Shadow", 20, 8000, 6.0m },
                    { 125, "A sword blazing with solar fury, incinerating entire armies in a single swing.", "Attack", null, "Equipment", null, "Sword of the Gods", "Ancient", "Weapon", "Adds bleed damage to enemenies when they strike, stacking over time", "Thorns", 20, 8050, 6.2m },
                    { 126, "A staff that channels the power of creation itself, capable of reshaping reality.", "Attack", null, "Equipment", null, "Staff of Creation's End", "Ancient", "Weapon", "Grants additional mana regeneration per turn", "Rejuvenation", 20, 8100, 5.5m }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BuffDuration", "ConsumableType", "Description", "InventoryId", "ItemCategory", "MonsterId", "Name", "Rarity", "TurnUsed", "Value", "Weight" },
                values: new object[,]
                {
                    { 301, null, "Mana", "A weak mana potion. Restores a small amount of mana instantly.", null, "Consumable", null, "Weak Mana Infusion", "Common", 0, 5, 0.1m },
                    { 302, null, "Mana", "An uncommon mana potion. Restores a moderate amount of mana instantly.", null, "Consumable", null, "Lesser Mana Infusion", "Uncommon", 0, 20, 0.2m },
                    { 303, null, "Mana", "A rare mana potion. Restores a significant amount of mana instantly.", null, "Consumable", null, "Mana Infusion", "Rare", 0, 80, 0.4m },
                    { 304, null, "Mana", "An epic mana potion. Restores a large amount of mana instantly.", null, "Consumable", null, "Greater Mana Infusion", "Epic", 0, 150, 0.6m },
                    { 305, null, "Mana", "A legendary mana potion. Restores a huge amount of mana instantly.", null, "Consumable", null, "Legendary Mana Infusion", "Legendary", 0, 350, 0.8m },
                    { 306, null, "Mana", "A mythic mana potion. Restores an extraordinary amount of mana instantly.", null, "Consumable", null, "Mythic Mana Infusion", "Mythic", 0, 600, 1.0m },
                    { 307, null, "Mana", "An ancient mana potion. Restores a legendary quantity of mana instantly.", null, "Consumable", null, "Ancient Mana Infusion", "Ancient", 0, 1000, 2.0m },
                    { 308, null, "Heal", "A weak healing potion. Restores a small amount of health instantly.", null, "Consumable", null, "Weak Healing Infusion", "Common", 0, 5, 0.1m },
                    { 309, null, "Heal", "An uncommon healing potion. Restores a moderate amount of health instantly.", null, "Consumable", null, "Lesser Healing Infusion", "Uncommon", 0, 20, 0.2m },
                    { 310, null, "Heal", "A rare healing potion. Restores a significant amount of health instantly.", null, "Consumable", null, "Healing Infusion", "Rare", 0, 80, 0.4m },
                    { 312, null, "Heal", "An epic healing potion. Restores a large amount of health instantly.", null, "Consumable", null, "Greater Healing Infusion", "Epic", 0, 150, 0.6m },
                    { 313, null, "Heal", "A legendary healing potion. Restores a huge amount of health instantly.", null, "Consumable", null, "Legendary Healing Infusion", "Legendary", 0, 350, 0.8m },
                    { 314, null, "Heal", "A mythic healing potion. Restores an extraordinary amount of health instantly.", null, "Consumable", null, "Mythic Healing Infusion", "Mythic", 0, 600, 1.0m },
                    { 315, null, "Heal", "An ancient healing potion. Restores a legendary quantity of health instantly.", null, "Consumable", null, "Ancient Healing Infusion", "Ancient", 0, 1000, 2.0m },
                    { 316, 1, "Attack", "A weak attack potion. Boosts attack power for 1 turn.", null, "Consumable", null, "Weak Fury Phial", "Common", 0, 5, 0.1m },
                    { 317, 2, "Attack", "An uncommon attack potion. Boosts attack power for 2 turns.", null, "Consumable", null, "Lesser Fury Phial", "Uncommon", 0, 20, 0.2m },
                    { 318, 3, "Attack", "A rare attack potion. Boosts attack power for 3 turns.", null, "Consumable", null, "Fury Phial", "Rare", 0, 80, 0.4m },
                    { 319, 4, "Attack", "An epic attack potion. Boosts attack power for 4 turns.", null, "Consumable", null, "Greater Fury Phial", "Epic", 0, 150, 0.6m },
                    { 320, 5, "Attack", "A legendary attack potion. Boosts attack power for 5 turns.", null, "Consumable", null, "Legendary Fury Phial", "Legendary", 0, 350, 0.8m },
                    { 321, 6, "Attack", "A mythic attack potion. Boosts attack power for 6 turns.", null, "Consumable", null, "Mythic Fury Phial", "Mythic", 0, 600, 1.0m },
                    { 322, 7, "Attack", "An ancient attack potion. Boosts attack power for 7 turns.", null, "Consumable", null, "Ancient Fury Phial", "Ancient", 0, 1000, 2.0m },
                    { 323, 1, "Defense", "A weak defense potion. Increases defense for 1 turn.", null, "Consumable", null, "Weak Fortitude Tonic", "Common", 0, 5, 0.1m },
                    { 324, 2, "Defense", "An uncommon defense potion. Increases defense for 2 turns.", null, "Consumable", null, "Lesser Fortitude Tonic", "Uncommon", 0, 20, 0.2m },
                    { 325, 3, "Defense", "A rare defense potion. Increases defense for 3 turns.", null, "Consumable", null, "Fortitude Tonic", "Rare", 0, 80, 0.4m },
                    { 326, 4, "Defense", "An epic defense potion. Increases defense for 4 turns.", null, "Consumable", null, "Greater Fortitude Tonic", "Epic", 0, 150, 0.6m },
                    { 327, 5, "Defense", "A legendary defense potion. Increases defense for 5 turns.", null, "Consumable", null, "Legendary Fortitude Tonic", "Legendary", 0, 350, 0.8m },
                    { 328, 6, "Defense", "A mythic defense potion. Increases defense for 6 turns.", null, "Consumable", null, "Mythic Fortitude Tonic", "Mythic", 0, 600, 1.0m },
                    { 329, 7, "Defense", "An ancient defense potion. Increases defense for 7 turns.", null, "Consumable", null, "Ancient Fortitude Tonic", "Ancient", 0, 1000, 2.0m }
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
                columns: new[] { "Id", "Description", "GrantedAbilityId", "InventoryId", "ItemCategory", "MonsterId", "Name", "Rarity", "Value", "Weight" },
                values: new object[,]
                {
                    { 201, "A blue hardcover book with gold inked pages", 1, null, "Spellbook", null, "Tome of the Aether", "Mythic", 0, 1.2m },
                    { 202, "A green tome, leaves etched into the cover", 2, null, "Spellbook", null, "Flora's Handbook", "Mythic", 0, 1.2m },
                    { 203, "A worn leather journal whose pages are as black as midnight ink", 3, null, "Spellbook", null, "Theif's Journal", "Mythic", 0, 1.2m },
                    { 204, "A heavy gray volume reinforced with iron plates", 4, null, "Spellbook", null, "Warrior's Codex", "Mythic", 0, 1.2m },
                    { 205, "A sleek crimson-bound tome that feels unnaturally cool to the touch", 5, null, "Spellbook", null, "Vampire's Tome", "Mythic", 0, 1.2m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Abilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Abilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Abilities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Abilities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Abilities",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
