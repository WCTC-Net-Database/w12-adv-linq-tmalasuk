using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using ConsoleRpgEntities.Models.Equipments;
using static ConsoleRpgEntities.Models.Equipments.Enums;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.Json;


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

        // Foreign key
        public int? EquipmentId { get; set; }
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
                Console.WriteLine("\n--- Items Menu ---");
                Console.WriteLine("1. Inventory");
                Console.WriteLine("2. Equipped Items");
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
                            Console.WriteLine("1. View Inventory");
                            Console.WriteLine("2. Inventory Management (Search/Sort)");
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
                                            var choice = Console.ReadLine();

                                            if (choice == "1")
                                            {
                                                consumable.Consume(this);
                                                break;
                                            }
                                            else if (choice == "2")
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
            Console.WriteLine($"\n--- {Name}'s Inventory ---");
            if (!Inventory.Items.Any())
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

            foreach (var item in Inventory.Items)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void InventoryManagement()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Inventory Management ---");
                Console.WriteLine("1. Search item by name");
                Console.WriteLine("2. List items by type");
                Console.WriteLine("3. Sort items (permanent)");
                Console.WriteLine("4. Exit");
                Console.Write("> ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Search
                        Console.Write("Enter item name to search: ");
                        var nameSearch = Console.ReadLine();
                        var matchingItems = Inventory.Items
                            .Where(i => i.Name.Contains(nameSearch, StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        if (matchingItems.Any())
                        {
                            Console.WriteLine("Matching items:");
                            foreach (var item in matchingItems)
                                Console.WriteLine($"- {item.Name} ({item.Type}, Value: {item.Value})");
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                        break;

                    case "2": // Group by type
                        var itemsByType = Inventory.Items
                            .GroupBy(i => i.Type)
                            .ToList();

                        foreach (var group in itemsByType)
                        {
                            Console.WriteLine($"\n{group.Key}:");
                            foreach (var item in group)
                                Console.WriteLine($"- {item.Name} (Value: {item.Value})");
                        }
                        break;

                    case "3": // Sort permanently
                        Console.WriteLine("\nSort by:");
                        Console.WriteLine("1. Name");
                        Console.WriteLine("2. Attack value (weapons only)");
                        Console.WriteLine("3. Defense value (armor only)");
                        Console.Write("> ");
                        var sortChoice = Console.ReadLine();

                        switch (sortChoice)
                        {
                            case "1":
                                Inventory.Items = Inventory.Items.OrderBy(i => i.Name).ToList();
                                break;

                            case "2":
                                Inventory.Items = Inventory.Items
                                    .OfType<Equipment>()
                                    .OrderByDescending(e => e.Type == Enums.EquipmentType.Attack ? e.Value : 0)
                                    .Cast<Item>()
                                    .ToList();
                                break;

                            case "3":
                                Inventory.Items = Inventory.Items
                                    .OfType<Equipment>()
                                    .OrderByDescending(e => e.Type == Enums.EquipmentType.Defense ? e.Value : 0)
                                    .Cast<Item>()
                                    .ToList();
                                break;

                            default:
                                Console.WriteLine("Invalid choice.");
                                continue;
                        }

                        Console.WriteLine("Inventory has been sorted permanently.");
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
            Inventory.Items.Add(item);
            Console.WriteLine($"{item.Name} added to {Name}'s inventory.");
        }



        public void EquipItem(Equipment item)
        {
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
