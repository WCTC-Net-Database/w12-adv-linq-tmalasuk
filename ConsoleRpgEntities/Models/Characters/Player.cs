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
        public virtual Inventory? Inventory { get; set; }
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
        public void ManageItems()
        {
            bool exitMasterMenu = false;
            

            while (!exitMasterMenu)
            {
                Console.WriteLine("\n--- Select ---");
                Console.WriteLine("1. Inventory");
                Console.WriteLine("2. Equipment");
                Console.WriteLine("3. Exit");
                Console.Write("> ");
                var masterChoice = Console.ReadLine();

                switch (masterChoice)
                {
                    case "1": // Inventory
                        bool exitInventoryMenu = false;

                        while (!exitInventoryMenu)
                        {
                            Console.WriteLine("\n--- Inventory Menu ---");
                            Console.WriteLine("1. View");
                            Console.WriteLine("2. Manage");
                            Console.WriteLine("3. Back");
                            Console.Write("> ");
                            var invChoice = Console.ReadLine();
                            

                            switch (invChoice)
                            {
                                case "1": // View inventory and interact
                                    ViewInventory();

                                    Console.WriteLine("\nEnter the name of an item to interact with it, or 'back' to return:");
                                    Console.Write("> ");
                                    var itemName = Console.ReadLine();
                                    if (itemName.Equals("back", StringComparison.OrdinalIgnoreCase))
                                        break;

                                    var selectedItem = Inventory.Items
                                        .FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

                                    if (selectedItem == null)
                                    {
                                        Console.WriteLine("Item not found.");
                                        break;
                                    }

                                    // Interact with the selected item
                                    if (selectedItem is Consumable consumable)
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine($"\nDo you want to consume {consumable.Name}?");
                                            Console.WriteLine("1. Yes");
                                            Console.WriteLine("2. No / Back");
                                            Console.Write("> ");
                                            var conchoice = Console.ReadLine();

                                            if (conchoice == "1")
                                            {
                                                Consume(consumable);
                                                break;
                                            }
                                            else if (conchoice == "2")
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid choice. Enter 1 or 2.");
                                            }
                                        }
                                    }
                                    else if (selectedItem is Equipment equipment)
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine($"\nDo you want to equip {equipment.Name}? (yes/no)");
                                            Console.Write("> ");
                                            var equipChoice = Console.ReadLine();

                                            if (equipChoice.Equals("yes", StringComparison.OrdinalIgnoreCase))
                                            {
                                                EquipItem(equipment);
                                                break;
                                            }
                                            else if (equipChoice.Equals("no", StringComparison.OrdinalIgnoreCase))
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid input. Type 'yes' or 'no'.");
                                            }
                                        }
                                    }
                                    break;

                                case "2": // Inventory Management (search/sort)
                                    InventoryManagement();
                                    break;

                                case "3": // Back
                                    exitInventoryMenu = true;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice. Try again.");
                                    break;
                            }
                        }
                        break;

                    case "2": // Equipped items
                        Console.WriteLine("\n--- Equipment Menu ---");
                        Console.WriteLine("1. View Equipped Items");
                        Console.WriteLine("2. View Equippable Items");
                        Console.WriteLine("3. Back");
                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                Console.WriteLine("\n--- Equipped Items ---");
                                foreach (Enums.EquipmentSlot slot in Enum.GetValues(typeof(Enums.EquipmentSlot)))
                                {
                                    if (Equipped.TryGetValue(slot, out var eq) && eq != null)
                                        Console.WriteLine($"{slot}: {eq.Name}");
                                    else
                                        Console.WriteLine($"{slot}: none");
                                }

                                Console.WriteLine("\nEnter slot name to unequip, or 'back' to return:");
                                Console.Write("> ");
                                var slotChoice = Console.ReadLine();
                                if (!slotChoice.Equals("back", StringComparison.OrdinalIgnoreCase) &&
                                    Enum.TryParse<Enums.EquipmentSlot>(slotChoice, true, out var slotEnum))
                                {
                                    if (Equipped.TryGetValue(slotEnum, out var itemToUnequip) && itemToUnequip != null)
                                    {
                                        UnequipItem(itemToUnequip); // UnequipItem now works with the actual Equipment
                                    }
                                    else
                                    {
                                        Console.WriteLine($"No item equipped in {slotEnum} slot.");
                                    }
                                }
                                break;
                            case "2":
                                var currentWeight = Equipped.Where(kv => kv.Value != null).Sum(kvp => kvp.Value.Weight);
                                if (currentWeight == null)
                                {
                                    currentWeight = 0;
                                }
                                var freeweight = EquipmentCarryLimit - currentWeight;
                                var equippableItems = Inventory.Items.Where(i => i.Weight < freeweight && i is Equipment).ToList();
                                if (equippableItems.Count > 0)
                                {
                                    Console.WriteLine($"{"Name",-25}{"Category",-12}{"Slot",-10}{"Stat",-20}{"Weight",6}");
                                    foreach (Item item in equippableItems)
                                    {
                                        Console.WriteLine(item);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("There are no items valid for equipping at this time");
                                }


                                break;

                        }

                        break;

                    case "3": // Exit master menu
                        exitMasterMenu = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        public void ViewInventory()
        {
            Console.WriteLine($"\n--- {Name}'s Inventory ---\n");
            Console.WriteLine($"{"Name",-25}{"Category",-12}{"Slot",-10}{"Stat",-20}{"Weight",6}");
            Console.WriteLine(new string('-', 73));

            foreach (var item in Inventory.Items)
            {
                Console.WriteLine(item);
            }
        }

        public void InventoryManagement()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Inventory Management ---");
                Console.WriteLine("1. Search");
                Console.WriteLine("2. Group Equipment by Slot");
                Console.WriteLine("3. Sort Iventory");
                Console.WriteLine("4. Back");
                Console.Write("> ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Search
                        Console.Write("Search: ");
                        var search = Console.ReadLine();
                        var matchingItems = Inventory.Items
                            .Where(i =>
                                // Name & Category
                                i.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                i.ItemCategory.Contains(search, StringComparison.OrdinalIgnoreCase) ||

                                // Equipment (Type or Slot)
                                (i is Equipment eq &&
                                    (eq.EquipmentType.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                        eq.Slot.ToString().Contains(search, StringComparison.OrdinalIgnoreCase))) ||

                                // Consumable Type
                                (i is Consumable c &&
                                    c.ConsumableType.ToString().Contains(search, StringComparison.OrdinalIgnoreCase))
                            )
                            .ToList();
                        if (matchingItems.Any())
                        {
                            Console.WriteLine("\n-----Matching items-----");
                            Console.WriteLine($"{"Name",-25}{"Category",-12}{"Slot",-10}{"Stat",-20}{"Weight",6}");
                            foreach (var item in matchingItems)
                                Console.WriteLine(item);
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                        break;

                    case "2": // List equipment grouped by slot
                        var equipmentItems = Inventory.Items
                            .OfType<Equipment>()
                            .GroupBy(e => e.Slot)
                            .ToList();

                        Console.WriteLine("\n--- Equipment By Slot ---");

                        foreach (var group in equipmentItems)
                        {
                            Console.WriteLine($"\n{group.Key}:"); // Slot name like Weapon, Head, Chest, etc.

                            foreach (var item in group)
                                Console.WriteLine(item);
                        }

                        break;
                    case "3": // Sort permanently
                        Console.WriteLine("\nSort by:");
                        Console.WriteLine("1. Name");
                        Console.WriteLine("2. Value");
                        Console.WriteLine("3. Weight");
                        Console.Write("> ");
                        var sortChoice = Console.ReadLine();

                        Console.WriteLine("\nOrder:");
                        Console.WriteLine("1. Ascending");
                        Console.WriteLine("2. Descending");
                        Console.Write("> ");
                        var orderChoice = Console.ReadLine();

                        bool descending = orderChoice == "2";

                        switch (sortChoice)
                        {
                            case "1":
                                if (descending)
                                    Inventory.Items = Inventory.Items.OrderByDescending(i => i.Name).ToList();
                                else
                                    Inventory.Items = Inventory.Items.OrderBy(i => i.Name).ToList();
                                break;

                            case "2":
                                if (descending)
                                    Inventory.Items = Inventory.Items.OrderByDescending(i => i.Value).ToList();
                                else
                                    Inventory.Items = Inventory.Items.OrderBy(i => i.Value).ToList();

                                break;

                            case "3":
                                if (descending)
                                    Inventory.Items = Inventory.Items.OrderByDescending(i => i.Weight).ToList();
                                else
                                    Inventory.Items = Inventory.Items.OrderBy(i => i.Weight).ToList();
                                break;

                            default:
                                Console.WriteLine("Invalid choice.");
                                continue;
                        }

                        Console.WriteLine("Inventory has been sorted permanently.");
                        exit = true;
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        

    }


        public void AddItemToInventory(Item item)
        {
            if (Inventory.InventoryWeight + item.Weight > InventoryCarryLimit)
            {
                Console.WriteLine($"{Name} cannot carry {item.Name}. Exceeds carry weight limit.");
                return;
            }
            Inventory.Items.Add(item);
            Console.WriteLine($"{item.Name} added to {Name}'s inventory.");
        }



        public void EquipItem(Equipment item)
        {
            var currentlyEquippedWeight = Equipped.Values.Where(e => e != null).Sum(e => e.Weight);
            if (currentlyEquippedWeight + item.Weight > EquipmentCarryLimit)
            {
                Console.WriteLine($"{Name} cannot equip {item.Name}. Exceeds equipment carry weight limit.");
                return;
            }
            var targetSlot = item.Slot.Value;
            if (Equipped.TryGetValue(targetSlot, out var currentlyEquipped) && currentlyEquipped != null)
            {
                UnequipItem(currentlyEquipped); // This should handle returning it to inventory
            }

            // Equip the new item
            Equipped[targetSlot] = item;
            Inventory.Items.Remove(item);
            Console.WriteLine($"{item.Name} equipped in {targetSlot} slot.");
        }

        public void Consume(Consumable consumable)
        {

            switch (consumable.ConsumableType)
            {
                case Enums.ConsumableType.Heal:
                    if (consumable is Item healItem)
                    {
                        Health += healItem.Value;
                        Console.WriteLine($"{Name} healed for {healItem.Value} health!");
                        //remove item
                        Inventory.Items.Remove(consumable);
                    }

                    break;
                case Enums.ConsumableType.Attack:
                    if (consumable is Item attackItem)
                    {
                        // Apply attack buff logic here
                        Console.WriteLine($"{Name}'s attack increased by {attackItem.Value} for {consumable.BuffDuration.Value} turns!");
                        //remove item
                        Inventory.Items.Remove(consumable);
                    }
                    break;
                case Enums.ConsumableType.Defense:
                    if (consumable is Item defenseItem)
                    {
                        // Apply defense buff logic here
                        Console.WriteLine($"{Name}'s defense increased by {defenseItem.Value} for {consumable.BuffDuration.Value} turns!");
                        //remove item
                        Inventory.Items.Remove(consumable);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown consumable type.");
                    break;
            }
        }

        public void UnequipItem(Equipment item)
        {
            var slotToEmpty = item.Slot.Value;
                Inventory.Items.Add(item);
                Equipped.Remove(slotToEmpty);
                Console.WriteLine($"{item.Name} unequipped from {item.Slot}.");
            
            
        }
        public void RemoveItemFromInventory(Item item)
        {
            Console.WriteLine("Which item would you like to drop?");
            foreach (var invItem in Inventory.Items)
            {
                Console.WriteLine($"- {invItem.Name}");
            }
            Console.Write("> ");
            var choice = Console.ReadLine();
            var itemToRemove = Inventory.Items.FirstOrDefault(i => i.Name.Equals(choice, StringComparison.OrdinalIgnoreCase));
        }
    }
}
