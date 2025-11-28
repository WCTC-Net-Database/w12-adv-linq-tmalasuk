using ConsoleRpg.Helpers.Battle;
using ConsoleRpg.Helpers.Menus;
using ConsoleRpgEntities.Models;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Containers;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.EntityHelper
{
    public class InventoryMenu
    {
        private readonly OutputManager _outputManager;
        public InventoryManager InventoryManager;
        private readonly PlayerManager _playerManager;
        private readonly RoomManager _roomManager;

        public InventoryMenu(OutputManager outputManager, InventoryManager inventoryManager, PlayerManager playerManager, RoomManager roomManager)
        {
            _outputManager = outputManager;
            InventoryManager = inventoryManager;
            _playerManager = playerManager;
            _roomManager = roomManager;
        }

        public void HandleInventorySelection(GameLoopMenu menu, OutputManager outputManager, Player player)
        {            
            while (true)
            {
                menu.SetMenuStateandRefresh("items");
                var choice = Console.ReadLine().ToLower();
                if (choice == "b")
                {
                    menu.SetMenuStateandRefresh("default");
                    break;
                }
                else if (choice == "v")
                {
                    InventoryManager.ViewInventory();
                    menu.AddEventsandRefresh(ItemListInteract(player.Inventory.Items.ToList()));                
                }
                else if (choice == "m")
                {
                    HandleItemsManagement(menu, outputManager, player);
                }
                    
                else
                {
                    menu.AddEventandRefresh("Invalid selection.");
                    continue;
                }

            }
        }

        private void HandleItemsManagement(GameLoopMenu menu, OutputManager outputManager, Player player)
        {
            
            while (true)
            {
                menu.SetMenuStateandRefresh("manage");
                var choice = Console.ReadLine().ToLower();
                if (choice == "q")
                {
                    menu.SetMenuStateandRefresh("query");
                    menu.AddEventsandRefresh(InventoryQueryMenu());
                    
                    var query = Console.ReadLine().ToLower();
                    var matchingItems = InventoryManager.SearchInventory(query);
                    if (matchingItems.Any())
                    {
                        menu.AddEventsandRefresh(ItemListInteract(matchingItems));

                    }
                    else
                    {
                        menu.AddEventandRefresh("No matches found");
                        break;
                    }

                }
                else if (choice == "s")
                {
                    HandleSort(menu, outputManager);
                    break;
                }
                else if (choice == "b")
                {
                    return;
                }
                else
                {
                    menu.AddEventandRefresh("Invalid selection.");
                    continue;
                }

            }
        }

        private void HandleSort(GameLoopMenu menu, OutputManager outputManager)
        {
            
            while (true)
            {
                menu.SetMenuStateandRefresh("sort");
                var typeSort = Console.ReadLine()?.ToLower();

                if (typeSort != "n" && typeSort != "w" && typeSort != "t" && typeSort != "c")
                {
                    menu.AddEventandRefresh("Not a valid selection");
                    continue; 
                }

                menu.SetMenuStateandRefresh("a/d");
                while (true)
                {
                   
                    var ad = Console.ReadLine()?.ToLower();

                    if (ad != "a" && ad != "d")
                    {
                        menu.AddEventandRefresh("Not a valid selection");
                        continue; 
                    }

                    // Perform sort
                    string sortField = typeSort switch
                    {
                        "n" or "name" => "Name",
                        "w" or "weight" => "Weight",
                        "t" or "type" => "Type",
                        "c" or "category" => "Category",
                        _ => typeSort
                    };

                    string sortDir = ad switch
                    {
                        "a" => "Ascending",
                        "d" => "Descending",
                        _ => ad
                    };

                    InventoryManager.SortItems(typeSort, ad);
                    menu.AddEventandRefresh($"Inventory sorted by {sortField} ({sortDir}).");

                    break; 
                }

                break; 
            }
        }


        public List<string> BuildItemInteractMenu(Item item, IItemContainer container)
        {        
            var menu = new List<string>();

            if (container is RoomItems)
            {
                menu.Add("[A] Add to Inventory");
            }
            if (container is Inventory)
            {
                menu.Add("[D] Drop From Inventory");
                if (item is Equipment)
                {
                    menu.Add("[E] Equip");
                }
                if (item is Consumable)
                {
                    menu.Add("[C] Consume");
                }
                if(item is Spellbook)
                {
                    menu.Add("[L] Learn");
                }
            }
            if (container is Equipped)
            {
                menu.Add("[U] Unequip");
            }
            menu.Add("[B] Back");
            return menu;

        }

        public IItemContainer? FindContainerForItem(Item item)
        {
            var containers = new IItemContainer[]
                {
                    _playerManager.Player.Inventory,    // Player’s backpack
                    _playerManager.Player.Equipped,     // Equipped gear
                    _roomManager.Room.DroppedLoot        // Items on the floor
                };

            foreach (var container in containers)
            {
                if (container.Items.Contains(item))
                    return container;
            }
            return null;
        }


        public List<string> ItemListInteract(List<Item> itemList)
        {
            List<string> messages = new List<string>();
            var itemToCheckContainer = itemList.FirstOrDefault();
            IItemContainer container = FindContainerForItem(itemToCheckContainer);
            string title = "";
            switch (container)
            {
                case Inventory: title = $"{_playerManager.Player.Name}'s Inventory"; break;
                case Equipped: title = $"{_playerManager.Player.Name}'s Gear"; break;
                case RoomItems: title = $"{_roomManager.Room.Name} Items"; break;
                
            }

            while (true)
            {
                if (itemList.Count == 0) { return messages; }

                DisplayItemList(itemList, title);

                int tableEndTop = Console.GetCursorPosition().Top;

                _outputManager.Write("\nEnter item name to interact or [B] to return: ");
                _outputManager.Display();

                string request = Console.ReadLine()?.Trim().ToLower();
                if (request == "b") { return messages; }
                var item = InventoryManager.ItemSelector(request, container);
                if (item == null)
                {
                    _outputManager.DisplayErrorBelow("Item not found.", tableEndTop);
                    continue;
                }

                List<string> interactionMsgs = HandleItemInteraction(item, container);
                foreach (string msg in interactionMsgs)
                {
                    messages.Add(msg);
                }

                if (!container.Items.Contains(item))
                {
                    itemList.Remove(item);
                }
            }
        }

        public List<string> HandleItemInteraction(Item item, IItemContainer container)
        {
            List<string> messages = new List<string>();      
            while (true)
            {
                ItemDisplay(item);
                List<string> menu = BuildItemInteractMenu(item, container);
                foreach (string m in menu)
                {
                    _outputManager.Write($" {m} ");
                }
                _outputManager.Display();
                var choice = Console.ReadLine().ToLower();


                if (choice == "b")
                {
                    return messages;
                }
                else if (choice == "a" && container is RoomItems)
                {
                    if (InventoryManager.AddItemToInventory(item))
                    {
                        messages.Add($"{item.Name} added to inventory");
                        container.Items.Remove(item);
                        return messages;
                    }
                    else
                    {
                        _outputManager.DisplayError("Not enough room in inventory.");
                    }
                }
                else if (choice == "d" && container is Inventory)
                {
                    messages.Add(InventoryManager.RemoveItemFromInventory(item));
                    _roomManager.Room.DroppedLoot.Items.Add(item);
                    return messages;
                }
                else if ((choice == "e" || choice == "c" || choice == "l") && container is Inventory)
                {
                    List<string> interactMsgs = InventoryManager.InteractWithItem(item);
                    foreach (string msg in interactMsgs)
                    {
                        messages.Add(msg);
                    }
                    return messages;
                }
                else if (choice == "u" && container is Equipped && item is Equipment e)
                {
                    messages.Add(InventoryManager.UnequipItem(e));
                    return messages;
                }
                else
                {
                    _outputManager.DisplayError("Invalid selection");
                    continue;
                }

            }
        }

        //modular reusable display
        public void DisplayItemList(List<Item> itemList, string title)
        {
            if (itemList == null || itemList.Count == 0)
            {
                _outputManager.DisplayError("No items to display.");
                return;
            }

            _outputManager.Clear();

            const int nameWidth = 35;
            const int categoryWidth = 12;
            const int slotWidth = 12;
            const int typeWidth = 15;
            const int statWidth = 10;
            const int weightWidth = 8;

            string header = $"| {"Name",-nameWidth} | {"Category",-categoryWidth} | {"Slot",-slotWidth} | {"Type",-typeWidth} | {"Stat Value",-statWidth} | {"Weight",-weightWidth} |";
            string separator = new string('-', header.Length);

            // Display header
            _outputManager.WriteLine($"\n--- {title} ---\n");
            _outputManager.WriteLine(separator);
            _outputManager.WriteLine(header);
            _outputManager.WriteLine(separator);

            foreach (var item in itemList)
            {
                var itemData = item.GetColumnData(item);
                var rarityColor = itemData.rarity.Values.FirstOrDefault();

                // Each column is written with exact width; no extra spaces
                _outputManager.Write("| ", ConsoleColor.Gray);
                _outputManager.Write(itemData.Name.PadRight(nameWidth), rarityColor);
                _outputManager.Write(" | ", ConsoleColor.Gray);

                _outputManager.Write(itemData.Category.PadRight(categoryWidth), ConsoleColor.White);
                _outputManager.Write(" | ", ConsoleColor.Gray);

                _outputManager.Write(itemData.Slot.PadRight(slotWidth), ConsoleColor.White);
                _outputManager.Write(" | ", ConsoleColor.Gray);

                _outputManager.Write(itemData.Type.PadRight(typeWidth), ConsoleColor.White);
                _outputManager.Write(" | ", ConsoleColor.Gray);

                _outputManager.Write(itemData.Stat.PadRight(statWidth), ConsoleColor.Cyan);
                _outputManager.Write(" | ", ConsoleColor.Gray);

                _outputManager.Write(itemData.Weight.ToString("F2").PadRight(weightWidth), ConsoleColor.White);
                _outputManager.WriteLine(" |", ConsoleColor.Gray);
            }

            _outputManager.WriteLine(separator);
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
