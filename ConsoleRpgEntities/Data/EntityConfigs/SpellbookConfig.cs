using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

public class SpellbookConfig : IEntityTypeConfiguration<Spellbook>
{
    public void Configure(EntityTypeBuilder<Spellbook> builder)
    {
        builder.HasData(
            // Spellbooks
            new Spellbook { Id = 201, Name = "Tome of the Aether", ItemCategory = "Spellbook", Weight = 1.2M, Value = 0, GrantedAbilityId = 1, Rarity = "Mythic", Description = "A blue hardcover book with gold inked pages"},
            new Spellbook { Id = 202, Name = "Flora's Handbook", ItemCategory = "Spellbook", Weight = 1.2M, Value = 0, GrantedAbilityId = 2, Rarity = "Mythic", Description = "A green tome, leaves etched into the cover" },
            new Spellbook { Id = 203, Name = "Theif's Journal", ItemCategory = "Spellbook", Weight = 1.2M, Value = 0, GrantedAbilityId = 3 ,Rarity = "Mythic", Description = "A worn leather journal whose pages are as black as midnight ink" },
            new Spellbook { Id = 204, Name = "Warrior's Codex", ItemCategory = "Spellbook", Weight = 1.2M, Value = 0, GrantedAbilityId = 4 , Rarity = "Mythic", Description = "A heavy gray volume reinforced with iron plates" },
            new Spellbook { Id = 205, Name = "Vampire's Tome", ItemCategory = "Spellbook", Weight = 1.2M, Value = 0, GrantedAbilityId = 5 , Rarity = "Mythic", Description = "A sleek crimson-bound tome that feels unnaturally cool to the touch" }

        );
    }
}