using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.Environments
{
    public class LootManager
    {
        private readonly Random _rng = new();

        // Rarity bands in ascending order
        private readonly string[] _rarities =
        {
        "common", "uncommon", "rare", "epic", "legendary", "mythic", "ancient"
    };

        // Adjust these curves to preference
        private double BaseRarityChance(int diff)
        {
            // Difficulty scales the chance of rolling *higher* rarity
            // Difficulty 1 → ~0–10%
            // Difficulty 20 → ~0–55%
            return Math.Clamp(0.1 + (diff * 0.025), 0.1, 0.55);
        }

        /// Picks a rarity based on difficulty
        private string RollRarity(int difficulty)
        {
            double upgradeChance = BaseRarityChance(difficulty);
            int rarityIndex = 0; // common

            while (rarityIndex < _rarities.Length - 1 &&
                   _rng.NextDouble() < upgradeChance)
            {
                rarityIndex++;
            }

            return _rarities[rarityIndex];
        }

        /// Consumable drop chance based on difficulty
        private bool RollConsumable(int difficulty)
        {
            // 40% at difficulty 1 → 80% at diff 20
            double chance = Math.Clamp(0.40 + (difficulty * 0.02), 0.40, 0.80);
            return _rng.NextDouble() < chance;
        }

        /// Equipment drop chance based on difficulty
        private bool RollEquipment(int difficulty)
        {
            // 10% at difficulty 1 → 60% at diff 20
            double chance = Math.Clamp(0.10 + (difficulty * 0.025), 0.10, 0.60);
            return _rng.NextDouble() < chance;
        }

        /// Spellbook chance based on abilities known
        public bool RollSpellbook(int abilitiesKnown)
        {
            // 0 abilities → 100%
            // 1 → 70%
            // 2 → 50%
            // 3 → 35%
            // 4 → 25%
            // 5+ → 15%
            double[] table = { 1.00, 0.70, 0.50, 0.35, 0.25, 0.15 };

            int index = Math.Min(abilitiesKnown, table.Length - 1);
            return _rng.NextDouble() < table[index];
        }

        /// Public method for generating loot results
        public List<string> GenerateLoot(Player player, Monster monster, InventoryManager inventory, Room room)
        {
            var results = new List<string>();
            int diff = monster.Difficulty;
            List<Item> itemDrops = new List<Item>();

            //-------------- GENERATE
            // 1. Consumable
            if (RollConsumable(diff))
            {
                string rarity = RollRarity(diff);
                Item item = inventory.GetRandomConsumable(rarity);
                if (item != null)
                {
                    results.Add($"{monster.Name} dropped {item.Name}!");
                    itemDrops.Add(item);
                }

            }

            // 2. Equipment
            if (RollEquipment(diff))
            {
                string rarity = RollRarity(diff);
                Item item = inventory.GetRandomEquipment(rarity);
                if (item != null)
                {
                    results.Add($"{monster.Name} dropped {item.Name}!");
                    itemDrops.Add(item);
                }

            }

            // 3. Spellbook
            int knownAbilities = player.Abilities.Count;

            if (RollSpellbook(knownAbilities))
            {
                Item spell = inventory.GetRandomSpellbook();
                if (spell != null)
                {
                    results.Add($"{monster.Name} dropped {spell.Name}!");
                    itemDrops.Add(spell);
                }

            }

            // -------------- ATTEMPT ADD
            foreach (Item item in itemDrops)
            {
                bool canAdd = inventory.AddItemToInventory(item);
                if (canAdd)
                {
                    results.Add($"{item.Name} added to inventory!");
                }
                else
                {
                    results.Add($"{item.Name} is too heavy to fit in your bag..."); 
                    room.DroppedLoot.Items.Add(item);
                }
            }

            return results;
        }
    }


}
