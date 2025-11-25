using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.EntityHelper
{
    public class InventoryMenu
    {
        private readonly OutputManager _outputManager;
        public InventoryManager _inventoryManager;
        private readonly PlayerManager _playerManager;

        public InventoryMenu(OutputManager outputManager, InventoryManager inventoryManager, PlayerManager playerManager)
        {
            _outputManager = outputManager;
            _inventoryManager = inventoryManager;
            _playerManager = playerManager;
        }

        public List<string> ItemListInteract(List<Item> itemList)
        {
            List<string> messages = new List<string>();

            while (true)
            {
                _outputManager.Clear();
                DisplayItemList(itemList);

                int tableEndTop = Console.GetCursorPosition().Top;

                _outputManager.WriteLine("\nEnter item name to interact or [B] to return:");
                _outputManager.Write(">> ");
                _outputManager.Display();

                string request = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(request))
                    continue;

                if (request == "b")
                    return messages;

                var item = _inventoryManager.ItemSelector(request);
                if (item == null)
                {
                    _outputManager.ClearBelow(tableEndTop);
                    _outputManager.WriteLine("Item not found.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }

                bool itemExit = false;

                while (!itemExit)
                {
                    ItemDisplay(item);

                    // Show menu per item type
                    if (item is Equipment)
                    {
                        if (!_inventoryManager.IsItemEquipped(item))
                            _outputManager.WriteLine("\n[E] Equip  [D] Drop  [B] Back");
                        else
                            _outputManager.WriteLine("\n[U] Unequip  [D] Drop  [B] Back");
                    }
                    else if (item is Consumable)
                    {
                        _outputManager.WriteLine("\n[C] Consume  [D] Drop  [B] Back");
                    }
                    else if (item is Spellbook)
                    {
                        _outputManager.WriteLine("\n[L] Learn  [D] Drop  [B] Back");
                    }

                    _outputManager.Write(">> ");
                    _outputManager.Display();

                    string choice = Console.ReadLine()?.Trim().ToLower();

                    switch (choice)
                    {
                        case "e":
                        case "u":
                        case "c":
                        case "l":
                            foreach (string msg in _inventoryManager.InteractWithItem(item))
                                messages.Add(msg);
                            itemList.Remove(item);
                            itemExit = true;
                            break;

                        case "d":
                            messages.Add(_inventoryManager.RemoveItemFromInventory(item));
                            itemList.Remove(item);
                            itemExit = true;
                            break;

                        case "b":
                            itemExit = true;
                            break;

                        default:
                            _outputManager.Clear();
                            _outputManager.WriteLine("Invalid selection.", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1200);
                            break;
                    }
                }
            }
        }


        public void DisplayItemList(List<Item> itemList)
        {
            Console.WriteLine($"\n--- {_playerManager.Player.Name}'s Inventory ---\n");
            Console.WriteLine(string.Format(
                Item.ColumnFormat,
                "Name",      // {0,-25}
                "Category",  // {1,-12}
                "Slot",      // {2,-12}
                "Type",      // {3,-10} (or "Stat" if you prefer)
                "Stat",      // {4,-20} (detailed stat like "Attack Power: 10")
                "Weight"     // {5,6:F2}
            ));

            Console.WriteLine(new string('-', 90)); // optional separator


            foreach (var item in itemList)
            {
                Console.WriteLine(item);
            }
        }
        public List<string> InventoryQueryMenu()
        {
            List<string> list = new List<string>();
            list.Add("You may search for any item by...");
            list.Add("-- Name");
            list.Add("-- Category (equipment/consumable/spellbook)");
            list.Add("-- Equipment Slot (Head/Chest/Legs/Feet/Hands/Weapon)");
            list.Add("-- Equipment Type (attack/defense)");
            list.Add("-- Consumable Type (heal/attack/defense)");
            return list;
        }
        

        public void ItemDisplay(Item item)
        {
            _outputManager.Clear();
            // 1. Determine which ASCII art to use
            string targetKey;
            if (item is Equipment equipment)
                targetKey = equipment.Slot.ToString();
            else if (item is Consumable)
                targetKey = "Potion";
            else if (item is Spellbook)
                targetKey = "Spellbook";
            else
                targetKey = "Weapon"; // fallback

            // 2. Get ASCII art array
            string[] art = AsciiArt.Art.ContainsKey(targetKey) ? AsciiArt.Art[targetKey] : new string[] { "No Art" };

            // 3. Prepare item details
            string[] details;

            if (item is Equipment equipment1)
            {
                details = new string[]
                {
            $"Name: {item.Name}",
            $"Category: {item.ItemCategory}",
            $"Slot: {equipment1.Slot}",
            $"Weight: {item.Weight}",
            $"{(equipment1.EquipmentType switch
            {
                Enums.EquipmentType.Attack => $"Damage: {equipment1.Value}",
                Enums.EquipmentType.Defense => $"Defense: {equipment1.Value}",
                _ => $"Stat: {equipment1.Value}"
            })}"
                };
            }
            else if (item is Consumable consumable1)
            {
                details = new string[]
                {
            $"Name: {item.Name}",
            $"Category: {item.ItemCategory}",
            $"Type: {consumable1.ConsumableType}",
            $"Weight: {item.Weight}",
            $"{(consumable1.ConsumableType switch
            {
                Enums.ConsumableType.Attack => $"Damage Buff: {consumable1.Value} for {consumable1.BuffDuration} turns",
                Enums.ConsumableType.Defense => $"Defense Buff: {consumable1.Value} for {consumable1.BuffDuration} turns",
                Enums.ConsumableType.Heal => $"Heal: {consumable1.Value}",
                _ => "Unknown Effect"
            })}"
                };
            }
            else if (item is Spellbook spellbook)
            {
                details = new string[]
                    {
                    $"Name: {item.Name}",
                    $"Category: {item.ItemCategory}",
                    $"Ability: **{spellbook.GrantedAbility.Name}**",
                    $"Type: {spellbook.GrantedAbility.AbilityType}",
                    $"Mana Cost: {spellbook.GrantedAbility.ManaCost}",
                    $"Description:",
                    $"{spellbook.GrantedAbility.Description}"
                    };
            }
            else
            {
                details = new string[]
                {
                $"Name: {item?.Name ?? "Unknown"}",
                $"Category: {item?.ItemCategory ?? "Unknown"}",
                "Slot: N/A",
                "Weight: N/A",
                "Stat: N/A"
                };
            }


            // 4. Determine how many lines to print
            int maxLines = Math.Max(art.Length, details.Length);

            // 5. Print each line
            Console.WriteLine();
            for (int i = 0; i < maxLines; i++)
            {
                string left = i < art.Length ? art[i] : "";
                string right = i < details.Length ? details[i] : "";
                Console.WriteLine($"{left,-30}  {right}");
            }
        }


    }
}
