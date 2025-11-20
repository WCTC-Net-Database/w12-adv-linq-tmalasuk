using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Equipments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using static ConsoleRpgEntities.Models.Equipments.Enums;


namespace ConsoleRpgEntities.Models.Characters
{

    public class EquipmentSettings
    {
        public Dictionary<EquipmentSlot, Equipment> Equipped { get; set; } = new();
    }

    public class Player : ITargetable, IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }

        [NotMapped]
        public int InventoryCarryLimit { get; set; } = 20;
        [NotMapped]
        public int EquipmentCarryLimit { get; set; } = 10;


        // Foreign key
        public int? InventoryId { get; set; } // Foreign key for Inventory


        // Navigation properties
        public virtual Inventory Inventory { get; set; } 
        public virtual ICollection<Ability> Abilities { get; set; }

        // Currently equipped items (one per slot)
        [NotMapped]
        public virtual Dictionary<EquipmentSlot, Equipment> Equipped { get; set; }


        public string EquippedJson
        {
            get => JsonSerializer.Serialize(Equipped, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
            set
            {
                if (string.IsNullOrEmpty(value))
                    Equipped = new Dictionary<EquipmentSlot, Equipment>();
                else
                    Equipped = JsonSerializer.Deserialize<Dictionary<EquipmentSlot, Equipment>>(value) ?? new Dictionary<EquipmentSlot, Equipment>();
            }
        }




        public void Attack(ITargetable target)
        {
            if (!Equipped.TryGetValue(EquipmentSlot.Weapon, out var weapon))
            {
                Console.WriteLine($"{Name} has no weapon equipped! They weakly punch at {target.Name}");
                return;
            }

            Console.WriteLine($"{Name} attacks {target.Name} with {weapon.Name}, dealing {weapon.Value} damage!");
            target.Health -= weapon.Value;
            Console.WriteLine($"{target.Name} has {target.Health} health remaining.");
        }


        public void UseAbility(IAbility ability, ITargetable target)
        {
            if (Abilities.Contains(ability))
            {
                ability.Activate(this, target);
            }
            else
            {
                Console.WriteLine($"{Name} does not have the ability {ability.Name}!");
            }
        }

        //------------------------------inventory methods-----------------------------------------
    }
}
