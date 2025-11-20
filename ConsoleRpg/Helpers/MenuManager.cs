using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;

namespace ConsoleRpg.Helpers;

public class MenuManager
{
    private readonly OutputManager _outputManager;
    private readonly InventoryManager _inventoryManager;

    public MenuManager(OutputManager outputManager, InventoryManager inventoryManager)
    {
        _outputManager = outputManager;
        _inventoryManager = inventoryManager;
    }

    public bool ShowMainMenu()
    {
        _outputManager.WriteLine("Welcome to the RPG Game!", ConsoleColor.Yellow);
        _outputManager.WriteLine("1. Start Game", ConsoleColor.Cyan);
        _outputManager.WriteLine("2. Exit", ConsoleColor.Cyan);
        _outputManager.Display();

        return HandleMainMenuInput();
    }

    private bool HandleMainMenuInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _outputManager.WriteLine("Starting game...", ConsoleColor.Green);
                    _outputManager.Display();
                    return true;
                case "2":
                    _outputManager.WriteLine("Exiting game...", ConsoleColor.Red);
                    _outputManager.Display();
                    Environment.Exit(0);
                    return false;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 1 or 2.", ConsoleColor.Red);
                    _outputManager.Display();
                    break;
            }
        }
    }

    public void ItemMenu()
    {
        _outputManager.WriteLine("\n--- Select ---", ConsoleColor.Cyan);
        _outputManager.WriteLine("1. Inventory");
        _outputManager.WriteLine("2. Equipment");
        _outputManager.WriteLine("3. Exit");
        _outputManager.Write("> ");
        _outputManager.Display();
        var menuchoice = Console.ReadLine() ?? string.Empty;
        switch (menuchoice)
        {
            case "1":
                _outputManager.WriteLine("Opening Inventory...", ConsoleColor.Green);
                _outputManager.Display();
                _outputManager.Clear();
                InventoryMenu();
                break;
            case "2":
                _outputManager.WriteLine("Opening Equipment...", ConsoleColor.Green);
                _outputManager.Display();
                _outputManager.Clear();
                EquipmentMenu();
                break;
            case "3":
                _outputManager.WriteLine("Exiting Menu...", ConsoleColor.Red);
                _outputManager.Display();
                _outputManager.Clear();
                break;
            default:
                _outputManager.WriteLine("Invalid selection. Please choose 1, 2, or 3.", ConsoleColor.Red);
                break;
        }
    }

    private void InventoryMenu()
    {
        _outputManager.WriteLine("\n--- Inventory Menu ---", ConsoleColor.Cyan);
        _outputManager.WriteLine("1. View");
        _outputManager.WriteLine("2. Manage");
        _outputManager.WriteLine("3. Back");
        _outputManager.Write("> ");
        _outputManager.Display();
        var menuChoice = Console.ReadLine();
        switch (menuChoice)
        {
            case "1":
                _outputManager.WriteLine("Viewing Inventory...", ConsoleColor.Green);
                _outputManager.Display();
                _outputManager.Clear();
                 var invItems = _inventoryManager.ViewInventory();
                ItemListInteract(invItems);          
                break;
            case "2":
                _outputManager.WriteLine("Managing Inventory...", ConsoleColor.Green);
                _outputManager.Display();
                _outputManager.Clear();
                InventoryManageMenu();

                break;
            case "3":
                _outputManager.WriteLine("Returning to previous menu...", ConsoleColor.Red);
                _outputManager.Display();
                _outputManager.Clear();
                
                ItemMenu();
                break;
            default:
                _outputManager.WriteLine("Invalid selection. Please choose 1, 2, or 3.", ConsoleColor.Red);
                _outputManager.Display();
                InventoryMenu();
                break;
        }
    }

    public void EquipmentMenu()
    {
        _outputManager.WriteLine("\n--- Equipment Menu ---", ConsoleColor.Cyan);
        _outputManager.WriteLine("1. View Equipped Items");
        _outputManager.WriteLine("2. View Equippable Items");
        _outputManager.WriteLine("2. Back");
        _outputManager.Write("> ");
        _outputManager.Display();
        var menuChoice = Console.ReadLine();
        switch (menuChoice)
        {
            case "1":
                _outputManager.WriteLine("Viewing Equipped Items...", ConsoleColor.Green);
                _outputManager.Display();
                _outputManager.Clear();
                var equippedItems = _inventoryManager.ViewEquippedItems();
                ItemListInteract(equippedItems);
                break;
            case "2":
                _outputManager.WriteLine("Viewing Equippable Items...", ConsoleColor.Green);
                _outputManager.Display();
                _outputManager.Clear();
                var equipableItems = _inventoryManager.ViewEquippableItems();
                ItemListInteract(equipableItems);
                break;
            case "3":
                _outputManager.WriteLine("Returning to previous menu...", ConsoleColor.Red);
                _outputManager.Display();
                _outputManager.Clear();
                ItemMenu();
                break;
            default:
                _outputManager.WriteLine("Invalid selection. Please choose 1 or 2.", ConsoleColor.Red);
                _outputManager.Display();
                EquipmentMenu();
                break;
        }
    }

    private void ItemListInteract(List<Item> itemList)
    {
        DisplayItemList(itemList);
        _outputManager.WriteLine("\nEnter the name of an item to interact with it, or 'back' to return:");
        _outputManager.Write("> ");
        _outputManager.Display();
        var request = Console.ReadLine();
        if (_inventoryManager.ItemSelector(request) != null && request.ToLower() != "back")
        {
            var item = _inventoryManager.ItemSelector(request);
            if (item == null)
            {
                _outputManager.WriteLine("Item not found in inventory.", ConsoleColor.Red);
                _outputManager.Display();
            }
            ItemDisplay(item);
            if (item is Equipment equipment)
            {
                if (!_inventoryManager.IsItemEquipped(item))
                {
                    _outputManager.WriteLine("\nDo you wish to 1.Equip 2.Drop or 3.Back?");
                    _outputManager.Display();
                }
                else
                {
                    _outputManager.WriteLine("\nDo you wish to 1.Unequip 2.Drop or 3.Back?");
                    _outputManager.Display();
                }
            }
            else if (item is Consumable)
            {
                _outputManager.WriteLine("\nDo you wish to 1.Consume 2.Drop or 3.Back?");
                _outputManager.Display();
            }
            _outputManager.Write("> ");
            _outputManager.Display();
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    _inventoryManager.InteractWithItem(item);
                    break;
                case "2":
                        _inventoryManager.RemoveItemFromInventory(item);
                    break;
                case "3":
                    _outputManager.WriteLine("Returning to Items...", ConsoleColor.Green);
                    _outputManager.Display();
                    _outputManager.Clear();
                    ItemListInteract(itemList);
                    break;
                default:
                    _outputManager.Clear();
                    _outputManager.WriteLine("Invalid selection. Please choose 1, 2, or 3.", ConsoleColor.Red);
                    _outputManager.Display();
                    break;
            }
        }
        else if (request != null && request.ToLower() == "back")
        {
            _outputManager.Clear();
            ItemMenu();
        }
        else
        {
            _outputManager.Clear();
            _outputManager.WriteLine("Invalid input. Please try again.", ConsoleColor.Red);
            _outputManager.Display();
            ItemListInteract(itemList);
        }
    }

    public void DisplayItemList(List<Item> itemList)
    {
        Console.WriteLine($"\n--- {_inventoryManager._player.Name}'s Inventory ---\n");
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

    public void InventoryManageMenu()
    {
        _outputManager.WriteLine("\n--- Inventory Management ---", ConsoleColor.Cyan);
        _outputManager.WriteLine("1. Search");
        _outputManager.WriteLine("2. Sort");
        _outputManager.WriteLine("3. Back");
        _outputManager.Write("> ");
        _outputManager.Display();
        var menuChoice = Console.ReadLine();
        switch (menuChoice)
        {
            case "1":
                _outputManager.Clear();
                _outputManager.WriteLine("You may search for any item by...\n-- Name\n-- Category (equipment/consumable)\n-- Equipment Slot (Head/Chest/Legs/Feet/Hands/Weapon)\n-- Equipment Type (attack/defense)\n-- Consumable Type (heal/attack/defense)");
                _outputManager.Write("Enter search term:");
                _outputManager.Display();
                var searchTerm = Console.ReadLine() ?? string.Empty;
                var matchingItems = _inventoryManager.SearchInventory(searchTerm);
                if (matchingItems.Any())
                {
                    ItemListInteract(matchingItems);

                }
                else
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("No items found.", ConsoleColor.Red);
                    _outputManager.Display();
                    InventoryManageMenu();
                }
                break;
            case "2":
                _outputManager.WriteLine("Sorting Inventory...", ConsoleColor.Green);
                _outputManager.Clear();
                _outputManager.WriteLine("\nSort by:", ConsoleColor.Cyan);
                _outputManager.WriteLine("1. Name");
                _outputManager.WriteLine("2. Value");
                _outputManager.WriteLine("3. Weight");
                _outputManager.Write("> ");
                _outputManager.Display();
                var sortChoice = Console.ReadLine();

                _outputManager.Clear();
                _outputManager.WriteLine("\nOrder:");
                _outputManager.WriteLine("1. Ascending");
                _outputManager.WriteLine("2. Descending");
                _outputManager.Write("> ");
                _outputManager.Display();
                var orderChoice = Console.ReadLine();

                bool descending = orderChoice == "2";
                _inventoryManager.SortItems(sortChoice, descending);
                _outputManager.WriteLine("Inventory sorted!", ConsoleColor.Green);
                _outputManager.Display();
                _outputManager.Clear();
                var inventoryItems = _inventoryManager.ViewInventory();
                ItemListInteract(inventoryItems);
                break;
            case "4":
                _outputManager.WriteLine("Returning to Inventory Menu...", ConsoleColor.Red);
                _outputManager.Display();
                _outputManager.Clear();
                InventoryMenu();
                break;
            default:
                _outputManager.WriteLine("Invalid selection. Please choose 1, 2, 3, or 4.", ConsoleColor.Red);
                _outputManager.Display();
                break;
        }
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
