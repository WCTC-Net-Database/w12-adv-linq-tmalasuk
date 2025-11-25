using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EquipmentConfig : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        //common
        builder.HasData(
        // ---------------------------
        // HEAD (Common)
        // ---------------------------
        new Equipment { Id = 1, Name = "Worn Leather Cap", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.5M, Value = 2, Description = "A simple leather cap, scuffed but serviceable." },
        new Equipment { Id = 2, Name = "Frayed Cloth Hood", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.3M, Value = 1, Description = "A thin hood offering minimal protection from blows." },
        new Equipment { Id = 3, Name = "Novice Guard Helm", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.0M, Value = 3, Description = "A cheap metal helm often issued to rookie militia." },

        // ---------------------------
        // CHEST (Common)
        // ---------------------------
        new Equipment { Id = 4, Name = "Traveler's Tunic", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 1.0M, Value = 4, Description = "A plain cloth tunic worn by most novice adventurers." },
        new Equipment { Id = 5, Name = "Padded Vest", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 1.5M, Value = 6, Description = "A quilted vest that provides slight impact protection." },
        new Equipment { Id = 6, Name = "Stitched Leather Jerkin", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 1.8M, Value = 5, Description = "A basic leather jerkin with uneven stitching." },

        // ---------------------------
        // LEGS (Common)
        // ---------------------------
        new Equipment { Id = 7, Name = "Rough Cloth Trousers", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 0.9M, Value = 2, Description = "Simple trousers made from coarse cloth." },
        new Equipment { Id = 8, Name = "Padded Leggings", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 1.2M, Value = 3, Description = "Light padding sewn into durable cloth makes these ideal for training." },
        new Equipment { Id = 9, Name = "Leather Breeches", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 1.5M, Value = 4, Description = "Sturdy breeches made from untreated leather." },

        // ---------------------------
        // FEET (Common)
        // ---------------------------
        new Equipment { Id = 10, Name = "Patchwork Boots", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 0.8M, Value = 3, Description = "Boots stitched together from mismatched scraps of leather." },
        new Equipment { Id = 11, Name = "Cloth Shoes", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 0.4M, Value = 1, Description = "Soft cloth shoes with thin soles." },
        new Equipment { Id = 12, Name = "Traveler’s Boots", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.0M, Value = 4, Description = "Reliable boots built for long walks on rough terrain." },

        // ---------------------------
        // HANDS (Common)
        // ---------------------------
        new Equipment { Id = 13, Name = "Roughhide Gloves", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.3M, Value = 1, Description = "Stiff gloves made of untreated hide." },
        new Equipment { Id = 14, Name = "Linen Wraps", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.1M, Value = 1, Description = "Simple hand wraps used to prevent blistering." },
        new Equipment { Id = 15, Name = "Sturdy Work Gloves", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.4M, Value = 2, Description = "Gloves originally meant for laborers, now repurposed for adventuring." },

        // ---------------------------
        // WEAPONS (Common)
        // ---------------------------
        new Equipment { Id = 16, Name = "Rusty Iron Dagger", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 0.7M, Value = 6, Description = "An old dagger with a chipped blade and patches of rust." },
        new Equipment { Id = 17, Name = "Wooden Training Sword", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 1.4M, Value = 6, Description = "A wooden practice sword used by recruits." },
        new Equipment { Id = 18, Name = "Iron Shortsword", Rarity = "Common", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 2.1M, Value = 6, Description = "A basic iron shortsword favored by town guards." }
        );


        //uncommon
        builder.HasData(
        // ----------------------------------
        // HEAD (Uncommon)
        // ----------------------------------
        new Equipment { Id = 19, Name = "Reinforced Leather Cap", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.7M, Value = 6, Description = "A leather cap strengthened with thin metal plates." },
        new Equipment { Id = 20, Name = "Traveler’s Scout Hood", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.4M, Value = 5, Description = "A durable hood favored by scouts and couriers." },
        new Equipment { Id = 21, Name = "Bronze Rim Helm", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.3M, Value = 8, Description = "A metal helm trimmed with bronze for added protection." },

        // ----------------------------------
        // CHEST (Uncommon)
        // ----------------------------------
        new Equipment { Id = 22, Name = "Reinforced Tunic", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 1.3M, Value = 7, Description = "A light tunic with added leather padding around the torso." },
        new Equipment { Id = 23, Name = "Hardened Leather Vest", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 2.0M, Value = 10, Description = "A vest treated and hardened for improved protection." },
        new Equipment { Id = 24, Name = "Bronze Studded Jerkin", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 2.2M, Value = 12, Description = "A leather jerkin reinforced with bronze studs." },

        // ----------------------------------
        // LEGS (Uncommon)
        // ----------------------------------
        new Equipment { Id = 25, Name = "Reinforced Trousers", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 1.2M, Value = 5, Description = "Cloth trousers fitted with extra stitching and padding." },
        new Equipment { Id = 26, Name = "Leather Guard Leggings", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 1.6M, Value = 7, Description = "Leggings strengthened with layered leather strips." },
        new Equipment { Id = 27, Name = "Bronze-Plated Greaves", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.4M, Value = 11, Description = "Greaves with bronze plating to protect against heavier strikes." },

        // ----------------------------------
        // FEET (Uncommon)
        // ----------------------------------
        new Equipment { Id = 28, Name = "Reinforced Leather Boots", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.1M, Value = 6, Description = "Sturdy boots lined with reinforced stitching." },
        new Equipment { Id = 29, Name = "Traveler’s Trail Boots", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 0.9M, Value = 7, Description = "Reliable boots with reinforced soles for long journeys." },
        new Equipment { Id = 30, Name = "Bronze-Toed Boots", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.4M, Value = 10, Description = "Boots fitted with bronze toe plates for extra durability." },

        // ----------------------------------
        // HANDS (Uncommon)
        // ----------------------------------
        new Equipment { Id = 31, Name = "Reinforced Gloves", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.4M, Value = 4, Description = "Leather gloves strengthened with extra padding." },
        new Equipment { Id = 32, Name = "Traveler’s Gripping Wraps", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.2M, Value = 3, Description = "Cloth wraps treated with resin for improved grip." },
        new Equipment { Id = 33, Name = "Bronze-Knuckle Gloves", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.6M, Value = 6, Description = "Gloves fitted with bronze plating over the knuckles." },

        // ----------------------------------
        // WEAPONS (Uncommon)
        // ----------------------------------
        new Equipment { Id = 34, Name = "Sharpened Iron Dagger", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 0.6M, Value = 7, Description = "A well-maintained dagger with a sharp, polished blade." },
        new Equipment { Id = 35, Name = "Balanced Shortsword", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 2.0M, Value = 10, Description = "A shortsword forged with care, perfectly weighted for quick strikes." },
        new Equipment { Id = 36, Name = "Honed Hatchet", Rarity = "Uncommon", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 2.3M, Value = 9, Description = "A compact hatchet with a keen edge used by woodsmen and fighters alike." }
        );


        //rare
        builder.HasData(
        // ----------------------------------
        // HEAD (Rare)
        // ----------------------------------
        new Equipment { Id = 37, Name = "Helm of the Vigilant", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.1M, Value = 35, Description = "A polished steel helm engraved with runes that sharpen awareness." },
        new Equipment { Id = 38, Name = "Moonsteel Visor", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.9M, Value = 38, Description = "A lightweight visor forged from moonlit alloy, cool to the touch." },
        new Equipment { Id = 39, Name = "Cowl of Whispering Winds", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.6M, Value = 40, Description = "A soft cowl that faintly rustles, granting clarity in the chaos of battle." },
        // ----------------------------------
        // CHEST (Rare)
        // ----------------------------------
        new Equipment { Id = 40, Name = "Azureguard Vest", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 2.4M, Value = 55, Description = "A reinforced vest dyed in deep azure, rumored to resist magical forces." },
        new Equipment { Id = 41, Name = "Stormhide Jerkin", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 2.0M, Value = 52, Description = "Leather jerkins made from creatures that roam storm peaks, crackling faintly." },
        new Equipment { Id = 42, Name = "Gilded Chainmail", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 3.1M, Value = 60, Description = "A fine chainmail interwoven with filaments of gold, both beautiful and sturdy." },
        // ----------------------------------
        // LEGS (Rare)
        // ----------------------------------
        new Equipment { Id = 43, Name = "Sablestride Leggings", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 1.6M, Value = 42, Description = "Dark leggings that grant silent steps, woven from shadowy fibers." },
        new Equipment { Id = 44, Name = "Ironweave Greaves", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.2M, Value = 48, Description = "Greaves woven with thin strands of metal, offering surprising flexibility." },
        new Equipment { Id = 45, Name = "Emberforged Trousers", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 1.8M, Value = 50, Description = "Heat-tempered trousers warm to the touch, rumored to bolster stamina." },

        // ----------------------------------
        // FEET (Rare)
        // ----------------------------------
        new Equipment { Id = 46, Name = "Winddancer Boots", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.0M, Value = 37, Description = "Boots that feel impossibly light, favored by scouts and couriers." },
        new Equipment { Id = 47, Name = "Stonegrip Sabatons", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 2.3M, Value = 45, Description = "Heavy sabatons that anchor the wearer firmly to the ground." },
        new Equipment { Id = 48, Name = "Runetread Sandals", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 0.7M, Value = 41, Description = "Sandals carved with tiny runes that pulse softly with magic." },

        // ----------------------------------
        // HANDS (Rare)
        // ----------------------------------
        new Equipment { Id = 49, Name = "Gloves of Subtlety", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.5M, Value = 33, Description = "Supple gloves used by spies; enhance precise movements." },
        new Equipment { Id = 50, Name = "Hammerfist Gauntlets", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 1.9M, Value = 47, Description = "Thick gauntlets reinforced with condensed steel plating." },
        new Equipment { Id = 51, Name = "Frostlace Mitts", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.6M, Value = 44, Description = "Gloves touched by frost magic, cold but empowering." },

        // ----------------------------------
        // WEAPONS (Rare)
        // ----------------------------------
        new Equipment { Id = 52, Name = "Silverbrand Dagger", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 0.9M, Value = 55, Description = "A razor-sharp dagger forged from pure silver alloy." },
        new Equipment { Id = 53, Name = "Oathkeeper Blade", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 2.4M, Value = 68, Description = "A tempered longsword carried by elite knights sworn to ancient vows." },
        new Equipment { Id = 54, Name = "Groveheart Staff", Rarity = "Rare", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 1.8M, Value = 63, Description = "A wooden staff grown from enchanted trees, warm with life magic." }

        );


        //epic
        builder.HasData(
        // ----------------------------------
        // HEAD (Epic)
        // ----------------------------------
        new Equipment { Id = 55, Name = "Crown of Starbound Insight", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.0M, Value = 120, Description = "A radiant circlet infused with starlight, sharpening the mind and senses." },
        new Equipment { Id = 56, Name = "Dragonbone Helm", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.6M, Value = 135, Description = "A helm crafted from polished dragonbone, offering powerful protection." },
        new Equipment { Id = 57, Name = "Oracle's Veiled Hood", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.7M, Value = 128, Description = "A silken hood that glows faintly, granting glimpses of future possibilities." },

        // ----------------------------------
        // CHEST (Epic)
        // ----------------------------------
        new Equipment { Id = 58, Name = "Astralplate Aegis", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 3.4M, Value = 210, Description = "A majestic chestplate forged with astral essence, resonating with cosmic force." },
        new Equipment { Id = 59, Name = "Phoenixheart Vest", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 2.1M, Value = 195, Description = "A fiery garment infused with rebirth magic, warm and pulsing with life." },
        new Equipment { Id = 60, Name = "Nightwarden Brigandine", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 2.8M, Value = 205, Description = "Midnight-black armor worn by elite shadow sentinels." },

        // ----------------------------------
        // LEGS (Epic)
        // ----------------------------------
        new Equipment { Id = 61, Name = "Celestial Greaves", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.2M, Value = 160, Description = "Greaves shimmering with star-metal dust, enhancing agility." },
        new Equipment { Id = 62, Name = "Emberstride Tassets", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 1.9M, Value = 172, Description = "Heated plates forged in volcanic flame, burning with unmatched vigor." },
        new Equipment { Id = 63, Name = "Stormgait Legguards", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.0M, Value = 168, Description = "Legguards wrapped in swirling storm energy that quickens every step." },

        // ----------------------------------
        // FEET (Epic)
        // ----------------------------------
        new Equipment { Id = 64, Name = "Skysprint Sabatons", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.4M, Value = 150, Description = "Sabatons that defy gravity subtly, letting the wearer move like the wind." },
        new Equipment { Id = 65, Name = "Moltenmarch Boots", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.7M, Value = 165, Description = "Boots heated from within, leaving glowing footprints in dark places." },
        new Equipment { Id = 66, Name = "Frostbound Walkers", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.2M, Value = 158, Description = "Boots that chill the ground beneath them, empowering steady movement." },

        // ----------------------------------
        // HANDS (Epic)
        // ----------------------------------
        new Equipment { Id = 67, Name = "Gauntlets of Arcane Might", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.9M, Value = 140, Description = "Gauntlets humming with arcane currents that enhance striking power." },
        new Equipment { Id = 68, Name = "Soulwoven Grips", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.6M, Value = 150, Description = "Gloves woven from spirit-thread, strengthening focus and precision." },
        new Equipment { Id = 69, Name = "Titanforge Handguards", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 1.5M, Value = 155, Description = "Massive handguards etched with titan runes, heavy but immensely protective." },

        // ----------------------------------
        // WEAPONS (Epic)
        // ----------------------------------
        new Equipment { Id = 70, Name = "Voidpiercer Dagger", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 0.8M, Value = 180, Description = "A dagger forged in shadow, its edge seemingly slicing through reality." },
        new Equipment { Id = 71, Name = "Solarbrand Greatblade", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 3.6M, Value = 225, Description = "A greatsword burning with solar fire, radiant even in darkness." },
        new Equipment { Id = 72, Name = "Elderroot Warstaff", Rarity = "Epic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 2.3M, Value = 210, Description = "A staff carved from an ancient living tree, pulsing with primal magic." }
        );
        //legendary

        builder.HasData(
        // ----------------------------------
        // HEAD (Legendary)
        // ----------------------------------
        new Equipment { Id = 73, Name = "Crown of Eternal Dominion", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.2M, Value = 180, Description = "A crown imbued with the essence of kings, granting unmatched wisdom and authority." },
        new Equipment { Id = 74, Name = "Helm of the Fallen Star", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.5M, Value = 190, Description = "Forged from the core of a fallen star, it radiates celestial power." },
        new Equipment { Id = 75, Name = "Veil of the Infinite Mind", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 0.9M, Value = 180, Description = "A veil that sharpens the intellect beyond mortal limits." },
        // ----------------------------------
        // CHEST (Legendary)
        // ----------------------------------
        new Equipment { Id = 76, Name = "Armor of the Celestial Sentinel", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 4.0M, Value = 212, Description = "An armor infused with celestial energy, nearly indestructible." },
        new Equipment { Id = 77, Name = "Dragonheart Plate", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 3.8M, Value = 215, Description = "Forged from the heart of a dragon, impervious to fire and ice." },
        new Equipment { Id = 78, Name = "Mantle of the Eternal Flame", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 3.2M, Value = 210, Description = "A mantle that burns with unending flame, empowering its wearer." },

        // ----------------------------------
        // LEGS (Legendary)
        // ----------------------------------
        new Equipment { Id = 79, Name = "Legplates of the Immortal Guardian", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.5M, Value = 215, Description = "Leg armor of a timeless guardian, offering unmatched mobility and protection." },
        new Equipment { Id = 80, Name = "Greaves of the Astral Phoenix", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.3M, Value = 220, Description = "Flaming greaves reborn from ashes, increasing agility and fire resistance." },
        new Equipment { Id = 81, Name = "Stormforged Legguards", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.4M, Value = 231, Description = "Crafted with stormsteel, crackling with lightning energy that empowers every step." },

        // ----------------------------------
        // FEET (Legendary)
        // ----------------------------------
        new Equipment { Id = 82, Name = "Boots of the Infinite Horizon", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.5M, Value = 199, Description = "Boots that allow the wearer to stride across great distances with ease." },
        new Equipment { Id = 83, Name = "Dragonwing Sabatons", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.7M, Value = 203, Description = "Sabatons that grant speed and lightness, as if walking on dragon wings." },
        new Equipment { Id = 84, Name = "Frostwind Walkers", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.4M, Value = 214, Description = "Boots that leave a trail of frost, chilling enemies nearby." },

        // ----------------------------------
        // HANDS (Legendary)
        // ----------------------------------
        new Equipment { Id = 85, Name = "Gauntlets of the Worldbreaker", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 1.2M, Value = 182, Description = "Gauntlets that grant immense strength, capable of breaking mountains." },
        new Equipment { Id = 86, Name = "Soulgrasp Gloves", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 0.9M, Value = 192, Description = "Gloves that can channel and manipulate the very essence of souls." },
        new Equipment { Id = 87, Name = "Titanclad Handguards", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 1.5M, Value = 185, Description = "Handguards forged from titan iron, offering unstoppable grip and defense." },

        // ----------------------------------
        // WEAPONS (Legendary)
        // ----------------------------------
        new Equipment { Id = 88, Name = "Soulreaper Blade", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 3.8M, Value = 250, Description = "A blade whispering with ghostly voices." },
        new Equipment { Id = 89, Name = "Sunforged Greatsword", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 4.0M, Value = 265, Description = "A greatsword burning with solar energy" },
        new Equipment { Id = 90, Name = "Staff of the Primordial Sage", Rarity = "Legendary", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 2.8M, Value = 255, Description = "A staff infused with ancient magic" }

        );
        //mythic

        builder.HasData(
        // ----------------------------------
        // HEAD (Mythic)
        // ----------------------------------
        new Equipment { Id = 91, Name = "Crown of the Celestial Emperor", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.3M, Value = 500, Description = "A crown woven from starlight.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 5 },
        new Equipment { Id = 92, Name = "Helm of the Eternal Sentinel", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.5M, Value = 510, Description = "A helm made with mythical steel, wings decorating each side", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 5 },
        new Equipment { Id = 93, Name = "Hood of Infinite Insight", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.0M, Value = 520, Description = "A hood that sharpens the mind to perceive truths hidden from mortals.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 5},

        // ----------------------------------
        // CHEST (Mythic)
        // ----------------------------------
        new Equipment { Id = 94, Name = "Armor of the Primordial Guardian", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 5.0M, Value = 600, Description = "A chestplate with an ornate carvings throughout", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 5 },
        new Equipment { Id = 95, Name = "Robe of the Arcane Well", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 4.8M, Value = 680, Description = "This flowing robe pulses with a quiet, magical energy.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 5 },
        new Equipment { Id = 96, Name = "Tunic of the Silent Stalker", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 4.2M, Value = 650, Description = "A tunic forged of the shadows.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 5 },

        // ----------------------------------
        // LEGS (Mythic)
        // ----------------------------------
        new Equipment { Id = 97, Name = "Legplates of the Timeless Guardian", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 3.0M, Value = 680, Description = "Black steel leg armor, wrapped in mythical magics.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 5 },
        new Equipment { Id = 98, Name = "Whisper of Time Leggings", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.8M, Value = 670, Description = "Leggings imbued with time magic, allowing for greater evasion.",  SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 5},
        new Equipment { Id = 99, Name = "Moonlit Silk Legwraps", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 2.9M, Value = 695, Description = "These flowing silk leggings shimmer faintly, humming with latent energy that replenishes the wearer’s magical reserves.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 5 },

        // ----------------------------------
        // FEET (Mythic)
        // ----------------------------------
        new Equipment { Id = 100, Name = "Boots of the Endless Horizon", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.6M, Value = 650, Description = "Boots that allow the wearer to traverse impossible distances effortlessly.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 5 },
        new Equipment { Id = 101, Name = "Dragonflight Sabatons", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.8M, Value = 665, Description = "Sabatons forged of mythical steel, thorns dotting its surface.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 5 },
        new Equipment { Id = 102, Name = "Starlight Treads", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 1.5M, Value = 640, Description = "Runes sewn into the fabric hum softly, feeding the wearer’s arcane energy continuously.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 5 },

        // ----------------------------------
        // HANDS (Mythic)
        // ----------------------------------
        new Equipment { Id = 103, Name = "Gauntlets of the Worldshaper", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 1.4M, Value = 680, Description = "Gauntlets forged with mythical steel, thorns dotting its surface.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 5 },
        new Equipment { Id = 104, Name = "Soulgrasp Mythic Gloves", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 1.0M, Value = 690, Description = "Gloves that manipulate soul energy.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 5 },
        new Equipment { Id = 105, Name = "Titanforge Handguards", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 1.6M, Value = 600, Description = "Handguards forged with sleek black metal, allowing one's hands to glide effortlessly through the air.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 5 },

        // ----------------------------------
        // WEAPONS (Mythic)
        // ----------------------------------
        new Equipment { Id = 106, Name = "Dagger of the Elusive Shadow", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 4.0M, Value = 1200, Description = "A slender dagger that guides its wielder’s movements, allowing them to evade attacks with uncanny precision..", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 5 },
        new Equipment { Id = 107, Name = "Sunfire Greatsword", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 4.2M, Value = 1225, Description = "A greatsword blazing with solar energy.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 5 },
        new Equipment { Id = 108, Name = "Staff of Primordial Forces", Rarity = "Mythic", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 3.0M, Value = 1210, Description = "A staff imbued with raw creation magic", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 5}

        );

        //ancient
        builder.HasData(
        // ----------------------------------
        // HEAD (Ancient)
        // ----------------------------------
        new Equipment { Id = 109, Name = "Crown of the Gods", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 2.0M, Value = 5000, Description = "A crown forged from the bones of fallen gods." , SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 20},
        new Equipment { Id = 110, Name = "Helm of Eternal Dominion", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 2.2M, Value = 5050, Description = "Helm that grants absolute authority over mortal and immortal alike, with unyielding protection.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 20 },
        new Equipment { Id = 111, Name = "Mask of the Voidseer", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Head, Weight = 1.8M, Value = 5100, Description = "A mask that reveals the hidden threads of fate, allowing the wearer to manipulate destiny.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 20 },

        // ----------------------------------
        // CHEST (Ancient)
        // ----------------------------------
        new Equipment { Id = 112, Name = "Armor of the Cosmos", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 8.0M, Value = 8000, Description = "A chestplate forged from the fabric of stars.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 20 },
        new Equipment { Id = 113, Name = "Dragon King’s Carapace", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 7.8M, Value = 7900, Description = "A leather chest piece forged of ancient magic, bending time around it.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 20 },
        new Equipment { Id = 114, Name = "Robe of the World Essence", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Chest, Weight = 7.5M, Value = 7800, Description = "A robe forged in ancient world magic." , SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 20 },

        // ----------------------------------
        // LEGS (Ancient)
        // ----------------------------------
        new Equipment { Id = 115, Name = "Legguards of the Eternal Colossus", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 5.0M, Value = 4500, Description = "Leg armor that grants unmatched speed, strength, and stability against any foe.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 20 },
        new Equipment { Id = 116, Name = "Astral Breeches of the Cosmos", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 4.8M, Value = 4550, Description = "Breeches imbued with cosmic energy.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 20 },
        new Equipment { Id = 117, Name = "Legplates of Ancient Steel", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Legs, Weight = 4.9M, Value = 4600, Description = "Ancient steel legplates with myseriously etched runes dotting its surface.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 20},

        // ----------------------------------
        // FEET (Ancient)
        // ----------------------------------
        new Equipment { Id = 118, Name = "Boots of the Infinite Horizon", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 2.5M, Value = 4200, Description = "Boots that allow the wearer to move faster than the eye can follow, crossing worlds.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 20},
        new Equipment { Id = 119, Name = "Dragonflight Winged Sabatons", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 2.7M, Value = 4250, Description = "Sabatons granting etched in torns.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 20},
        new Equipment { Id = 120, Name = "Slippers of Eternity", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Feet, Weight = 2.4M, Value = 4300, Description = "Ancient slippers wrapped in magic from the gods.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 20 },

        // ----------------------------------
        // HANDS (Ancient)
        // ----------------------------------
        new Equipment { Id = 121, Name = "Claws of the Worldshaper", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 2.2M, Value = 4600, Description = "Leather groves with diamond tipped claws that can redirect attacks at whim.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 20},
        new Equipment { Id = 122, Name = "Soulgrasp Gloves of Eternity", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 2.0M, Value = 4650, Description = "Gloves that manipulate souls and channel all forms of energy.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 20 },
        new Equipment { Id = 123, Name = "Titanforge Gauntlets", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Defense, Slot = Enums.EquipmentSlot.Hands, Weight = 2.5M, Value = 4700, Description = "Handguards forged from primordial titan metal, unmatched in strength and durability.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 20 },

        // ----------------------------------
        // WEAPONS (Ancient)
        // ----------------------------------
        new Equipment { Id = 124, Name = "Dagger of the Infinite Abyss", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 6.0M, Value = 8000, Description = "A dagger that devours all matter it touches, leaving only void in its wake.", SpecialType = "Shadow", SpecialDescription = "Grants additional dodge chance", SpecialValue = 20},
        new Equipment { Id = 125, Name = "Sword of the Gods", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 6.2M, Value = 8050, Description = "A sword blazing with solar fury, incinerating entire armies in a single swing.", SpecialType = "Thorns", SpecialDescription = "Adds bleed damage to enemenies when they strike, stacking over time", SpecialValue = 20},
        new Equipment { Id = 126, Name = "Staff of Creation's End", Rarity = "Ancient", ItemCategory = "Equipment", EquipmentType = Enums.EquipmentType.Attack, Slot = Enums.EquipmentSlot.Weapon, Weight = 5.5M, Value = 8100, Description = "A staff that channels the power of creation itself, capable of reshaping reality.", SpecialType = "Rejuvenation", SpecialDescription = "Grants additional mana regeneration per turn", SpecialValue = 20 }

        );
    }
}