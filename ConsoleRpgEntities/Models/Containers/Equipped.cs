using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Containers
{
    public class Equipped : IItemContainer
    {
        public int Id { get; set; }

        // Foreign key
        public int PlayerId { get; set; }

        public virtual Equipment? Head { get; set; }
        public virtual Equipment? Chest { get; set; }

        public virtual Equipment? Legs { get; set; }

        public virtual Equipment? Feet { get; set; }

        public virtual Equipment? Hands { get; set; }

        public virtual Equipment? Weapon { get; set; }

        // Navigation properties
        public virtual Player Player { get; set; }


        [NotMapped]
        public virtual ICollection<Item> Items
        {
            get
            {
                return new List<Item> { Head, Chest, Legs, Feet, Hands, Weapon }
                       .Where(i => i != null)
                       .ToList();
            }
        }

        public decimal EquipmentWeight => Items.Sum(i => i!.Weight);

        public Equipment GetEquipmentFromSlot(string slot)
        {
            switch(slot){
                case "Head": return Head;
                case "Chest": return Chest;
                case "Legs": return Legs;
                case "Feet": return Feet;
                case "Hands": return Hands;
                case "Weapon": return Weapon;
                default: return null;
            }
        }

        public void AssignEquipmentToSlot(Equipment e)
        {
            switch (e.Slot)
            {
                case Enums.EquipmentSlot.Head: Head = e; break;
                case Enums.EquipmentSlot.Chest: Chest = e; break;
                case Enums.EquipmentSlot.Legs: Legs = e; break;
                case Enums.EquipmentSlot.Feet: Feet = e; break;
                case Enums.EquipmentSlot.Hands: Hands = e; break;
                case Enums.EquipmentSlot.Weapon: Weapon = e; break;
            }
        }

        public void ClearSlot(string slot)
        {
            switch (slot)
            {
                case "Head": Head = null; break;
                case "Chest": Chest = null; break;
                case "Legs": Legs = null; break;
                case "Feet": Feet = null; break;
                case "Hands": Hands = null; break;
                case "Weapon": Weapon = null; break;
            }
        }

        
    }
}
