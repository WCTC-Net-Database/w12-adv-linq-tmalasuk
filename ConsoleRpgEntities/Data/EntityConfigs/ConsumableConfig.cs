using ConsoleRpgEntities.Models;
using ConsoleRpgEntities.Models.Equipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsumableConfig : IEntityTypeConfiguration<Consumable>
{
    public void Configure(EntityTypeBuilder<Consumable> builder)
    {
        //-------------mana----------------
        builder.HasData(
            new Consumable { Id = 301, Name = "Weak Mana Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Mana, Weight = 0.1M, Value = 5, Rarity = "Common", Description = "A weak mana potion. Restores a small amount of mana instantly." },
            new Consumable { Id = 302, Name = "Lesser Mana Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Mana, Weight = 0.2M, Value = 20, Rarity = "Uncommon", Description = "An uncommon mana potion. Restores a moderate amount of mana instantly." },
            new Consumable { Id = 303, Name = "Mana Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Mana, Weight = 0.4M, Value = 80, Rarity = "Rare", Description = "A rare mana potion. Restores a significant amount of mana instantly." },
            new Consumable { Id = 304, Name = "Greater Mana Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Mana, Weight = 0.6M, Value = 150, Rarity = "Epic", Description = "An epic mana potion. Restores a large amount of mana instantly." },
            new Consumable { Id = 305, Name = "Legendary Mana Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Mana, Weight = 0.8M, Value = 350, Rarity = "Legendary", Description = "A legendary mana potion. Restores a huge amount of mana instantly." },
            new Consumable { Id = 306, Name = "Mythic Mana Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Mana, Weight = 1.0M, Value = 600, Rarity = "Mythic", Description = "A mythic mana potion. Restores an extraordinary amount of mana instantly." },
            new Consumable { Id = 307, Name = "Ancient Mana Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Mana, Weight = 2.0M, Value = 1000, Rarity = "Ancient", Description = "An ancient mana potion. Restores a legendary quantity of mana instantly." }
        );

        //------------health---------------
        builder.HasData(
            new Consumable { Id = 308, Name = "Weak Healing Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Heal, Weight = 0.1M, Value = 5, Rarity = "Common", Description = "A weak healing potion. Restores a small amount of health instantly." },
            new Consumable { Id = 309, Name = "Lesser Healing Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Heal, Weight = 0.2M, Value = 20, Rarity = "Uncommon", Description = "An uncommon healing potion. Restores a moderate amount of health instantly." },
            new Consumable { Id = 310, Name = "Healing Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Heal, Weight = 0.4M, Value = 80, Rarity = "Rare", Description = "A rare healing potion. Restores a significant amount of health instantly." },
            new Consumable { Id = 312, Name = "Greater Healing Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Heal, Weight = 0.6M, Value = 150, Rarity = "Epic", Description = "An epic healing potion. Restores a large amount of health instantly." },
            new Consumable { Id = 313, Name = "Legendary Healing Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Heal, Weight = 0.8M, Value = 350, Rarity = "Legendary", Description = "A legendary healing potion. Restores a huge amount of health instantly." },
            new Consumable { Id = 314, Name = "Mythic Healing Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Heal, Weight = 1.0M, Value = 600, Rarity = "Mythic", Description = "A mythic healing potion. Restores an extraordinary amount of health instantly." },
            new Consumable { Id = 315, Name = "Ancient Healing Infusion", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Heal, Weight = 2.0M, Value = 1000, Rarity = "Ancient", Description = "An ancient healing potion. Restores a legendary quantity of health instantly." }
        );

        //------------attack---------------
        builder.HasData(
            new Consumable { Id = 316, Name = "Weak Fury Phial", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Attack, Weight = 0.1M, Value = 5, BuffDuration = 1, Rarity = "Common", Description = "A weak attack potion. Boosts attack power for 1 turn." },
            new Consumable { Id = 317, Name = "Lesser Fury Phial", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Attack, Weight = 0.2M, Value = 20, BuffDuration = 2, Rarity = "Uncommon", Description = "An uncommon attack potion. Boosts attack power for 2 turns." },
            new Consumable { Id = 318, Name = "Fury Phial", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Attack, Weight = 0.4M, Value = 80, BuffDuration = 3, Rarity = "Rare", Description = "A rare attack potion. Boosts attack power for 3 turns." },
            new Consumable { Id = 319, Name = "Greater Fury Phial", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Attack, Weight = 0.6M, Value = 150, BuffDuration = 4, Rarity = "Epic", Description = "An epic attack potion. Boosts attack power for 4 turns." },
            new Consumable { Id = 320, Name = "Legendary Fury Phial", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Attack, Weight = 0.8M, Value = 350, BuffDuration = 5, Rarity = "Legendary", Description = "A legendary attack potion. Boosts attack power for 5 turns." },
            new Consumable { Id = 321, Name = "Mythic Fury Phial", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Attack, Weight = 1.0M, Value = 600, BuffDuration = 6, Rarity = "Mythic", Description = "A mythic attack potion. Boosts attack power for 6 turns." },
            new Consumable { Id = 322, Name = "Ancient Fury Phial", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Attack, Weight = 2.0M, Value = 1000, BuffDuration = 7, Rarity = "Ancient", Description = "An ancient attack potion. Boosts attack power for 7 turns." }
        );

        //------------defense--------------
        builder.HasData(
            new Consumable { Id = 323, Name = "Weak Fortitude Tonic", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Defense, Weight = 0.1M, Value = 5, BuffDuration = 1, Rarity = "Common", Description = "A weak defense potion. Increases defense for 1 turn." },
            new Consumable { Id = 324, Name = "Lesser Fortitude Tonic", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Defense, Weight = 0.2M, Value = 20, BuffDuration = 2, Rarity = "Uncommon", Description = "An uncommon defense potion. Increases defense for 2 turns." },
            new Consumable { Id = 325, Name = "Fortitude Tonic", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Defense, Weight = 0.4M, Value = 80, BuffDuration = 3, Rarity = "Rare", Description = "A rare defense potion. Increases defense for 3 turns." },
            new Consumable { Id = 326, Name = "Greater Fortitude Tonic", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Defense, Weight = 0.6M, Value = 150, BuffDuration = 4, Rarity = "Epic", Description = "An epic defense potion. Increases defense for 4 turns." },
            new Consumable { Id = 327, Name = "Legendary Fortitude Tonic", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Defense, Weight = 0.8M, Value = 350, BuffDuration = 5, Rarity = "Legendary", Description = "A legendary defense potion. Increases defense for 5 turns." },
            new Consumable { Id = 328, Name = "Mythic Fortitude Tonic", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Defense, Weight = 1.0M, Value = 600, BuffDuration = 6, Rarity = "Mythic", Description = "A mythic defense potion. Increases defense for 6 turns." },
            new Consumable { Id = 329, Name = "Ancient Fortitude Tonic", ItemCategory = "Consumable", ConsumableType = Enums.ConsumableType.Defense, Weight = 2.0M, Value = 1000, BuffDuration = 7, Rarity = "Ancient", Description = "An ancient defense potion. Increases defense for 7 turns." }
        );
    }
}