using ConsoleRpg.Helpers.Battle;
using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpg.Helpers.Environments;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Data.Seeding;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Models.Rooms.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleRpgEntities.Models.Equipments.Enums;

namespace ConsoleRpg.Helpers.Menus
{
    public class GameLoopMenu
    {

        private readonly OutputManager _outputManager;
        private readonly InventoryMenu _inventoryMenu;
        private readonly PlayerMenu _playerMenu;
        private readonly BattleManager _battleManager;
        private readonly MonsterManager _monsterManager;
        public static RoomManager _roomManager;
        private readonly PlayerManager _playerManager;
        private readonly GameContext _context;
        public static List<string> EventLog;
        public static string MenuState { get; set; }

        public GameLoopMenu(OutputManager outputManager, InventoryMenu inventoryMenu, PlayerMenu playerMenu, BattleManager battleManager, MonsterManager monsterManager, RoomManager roomManager, PlayerManager playerManager, GameContext context)
        {
            _inventoryMenu = inventoryMenu;
            _outputManager = outputManager;
            _playerMenu = playerMenu;
            _battleManager = battleManager;
            _monsterManager = monsterManager;
            _roomManager = roomManager;
            _playerManager = playerManager;
            _context = context;
            EventLog = new List<string>();
            MenuState = "default";

        }

        public void MainMenu()
        {
            setInstance();

            while (true)
            {
                Console.Clear();
                Render(_playerManager.Player, _roomManager.Room, EventLog);

                var input = Console.ReadLine().ToLower();

                if (input == "a")
                {
                    var monster = _roomManager.Room.MonstersInRoom.FirstOrDefault();
                    if (monster == null)
                    {
                        EventLog.Add("No monsters left in the room to attack.");
                        return;
                    }
                    _monsterManager.Monster = monster;

                    _battleManager.BattleLoop();

                    List<string> endEvents = _battleManager.HandleMonsterDefeat(_playerManager.Player, _monsterManager.Monster, _inventoryMenu._inventoryManager);
                    _roomManager.Room.MonstersInRoom.Remove(monster);
                    foreach (string line in endEvents)
                    {
                        EventLog.Add(line);
                    }
                }
                else if (input == "e")
                {
                    
                    Console.Clear();
                    MenuState = "explore";
                    var messages = _roomManager.ExploreRoom(_playerManager.Player);
                    foreach (var msg in messages)
                    {
                        EventLog.Add(msg);
                    }
                    Render(_playerManager.Player, _roomManager.Room, EventLog);
                    var input2 = Console.ReadLine().ToLower();
                    if (input2 == "i")
                    {
                        var intMessages = _roomManager.InteractWithAttribute(_playerManager.Player, "intelligence");
                        foreach (var msg in intMessages)
                        {
                            EventLog.Add(msg);
                        }
                    }
                    else if (input2 == "s")
                    {
                        var intMessages = _roomManager.InteractWithAttribute(_playerManager.Player, "strength");
                        foreach (var msg in intMessages)
                        {
                            EventLog.Add(msg);
                        }
                    }
                    else if (input2 == "a")
                    {
                        var intMessages = _roomManager.InteractWithAttribute(_playerManager.Player, "agility");
                        foreach (var msg in intMessages)
                        {
                            EventLog.Add(msg);
                        }
                    }
                    else
                    {
                        EventLog.Add("Invalid input. Returning to menu");
                        
                    }
                    MenuState = "default";
                }
                else if (input == "u")
                {
                    MenuState = "unlock";
                    Console.Clear();
                    Render(_playerManager.Player, _roomManager.Room, EventLog);
                    var choice = Console.ReadLine().ToLower();
                    if (_roomManager.Room is ILockedRoom lockedRoom)
                    {
                        var messages = lockedRoom.TryUnlock(choice, _playerManager.Player);
                        foreach (string line in messages)
                        {
                            EventLog.Add(line);
                        }
                    }
                    MenuState = "default";

                }
                else if (input == "i")
                {
                    while (true)
                    {
                        MenuState = "inventory";
                        Console.Clear();
                        Render(_playerManager.Player, _roomManager.Room, EventLog);
                        var choice = Console.ReadLine().ToLower();
                        if (choice == "b")
                        {
                            MenuState = "default";
                            return;
                        }
                        if (choice == "i")
                        {
                            while (true)
                            {
                                MenuState = "items";
                                Console.Clear();
                                Render(_playerManager.Player, _roomManager.Room, EventLog);
                                var choice2 = Console.ReadLine().ToLower();

                                if (choice2 == "b")
                                {
                                    return;
                                }
                                else if (choice2 == "v")
                                {
                                    var invItems = _inventoryMenu._inventoryManager.ViewInventory();
                                    var messages = _inventoryMenu.ItemListInteract(invItems);
                                    foreach (string line in messages)
                                    {
                                        EventLog.Add(line);
                                    }
                                    break;
                                }
                                else if (choice2 == "m")
                                    while (true)
                                    {
                                        MenuState = "manage";
                                        Console.Clear();
                                        Render(_playerManager.Player, _roomManager.Room, EventLog);
                                        var choice3 = Console.ReadLine().ToLower();
                                        if (choice3 == "q")
                                        {
                                            var queryInstructions = _inventoryMenu.InventoryQueryMenu();
                                            foreach (string line in queryInstructions)
                                            {
                                                EventLog.Add(line);
                                            }
                                            MenuState = "query";
                                            Render(_playerManager.Player, _roomManager.Room, EventLog);
                                            var query = Console.ReadLine().ToLower();
                                            var matchingItems = _inventoryMenu._inventoryManager.SearchInventory(query);
                                            if (matchingItems.Any())
                                            {
                                                List<string> invMessages = _inventoryMenu.ItemListInteract(matchingItems);
                                                foreach (string line in invMessages)
                                                {
                                                    EventLog.Add(line);
                                                }
                                                break;

                                            }
                                            else
                                            {
                                                EventLog.Add("No mathces found");
                                                break;
                                            }

                                        }
                                        else if (choice3 == "s")
                                        {
                                            MenuState = "sort";
                                            Render(_playerManager.Player, _roomManager.Room, EventLog);
                                            var typeSort = Console.ReadLine().ToLower();
                                            MenuState = "a/d";
                                            Render(_playerManager.Player, _roomManager.Room, EventLog);
                                            var ad = Console.ReadLine().ToLower();
                                            _inventoryMenu._inventoryManager.SortItems(typeSort, ad);
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

                                            EventLog.Add($"Inventory sorted by {sortField} ({sortDir}).");

                                            break;


                                        }
                                        else if (choice3 == "b")
                                        {
                                            return;
                                        }

                                    }
                                else
                                {
                                    EventLog.Add("Unreconized request");
                                    break;
                                }

                            }
                        }
                        if (choice == "e")
                        {
                            MenuState = "equipment";
                            Render(_playerManager.Player, _roomManager.Room, EventLog);
                            var equippedItems = _inventoryMenu._inventoryManager.ViewEquippedItems();
                            _inventoryMenu.ItemListInteract(equippedItems);
                        }
                    }


                }
                else if (input == "h")
                {
                    _playerMenu.MainMenu();
                }
                else if (input == "m") {

                    MenuState = "map";
                    Render(_playerManager.Player, _roomManager.Room, EventLog);
                    var direction = Console.ReadLine().ToLower();
                    List<string> messages = new List<string>();
                    switch (direction) {
                        case "n": foreach (string line in _roomManager.GoDirection("North", _playerManager.Player)) {messages.Add(line);}; break;
                        case "e": foreach (string line in _roomManager.GoDirection("East", _playerManager.Player)) { messages.Add(line); }; break;
                        case "s": foreach (string line in _roomManager.GoDirection("South", _playerManager.Player)) { messages.Add(line); }; break;
                        case "w": foreach (string line in _roomManager.GoDirection("West", _playerManager.Player)) { messages.Add(line); }; break;
                        case "u": foreach (string line in _roomManager.GoDirection("Up", _playerManager.Player)) { messages.Add(line); }; break;
                        case "d": foreach (string line in _roomManager.GoDirection("Down", _playerManager.Player)) { messages.Add(line); }; break;
                        default: messages.Add("Invalid Input"); break;
                    }
                    foreach (string line in messages) { EventLog.Add(line); }
                    setInstance();
                    MenuState = "default";
                    Render(_playerManager.Player, _roomManager.Room, EventLog);
                }
                    

            }
        }


        public void setInstance()
        {

            var rooms = _context.Rooms.ToList();

            if (_playerManager.Player.CurrentRoom != null)
            {
                _roomManager.Room = _playerManager.Player.CurrentRoom;
            }
            else
            {
                _roomManager.Room = _context.Rooms.FirstOrDefault(r => r.Id == 1);

            }

            // Load rooms from the database


        }
        public static List<string> BuildMenu(Room room)
        {
            var menu = new List<string>();

            // Always available
            if (MenuState == "default")
            {
                if (room is ICombatRoom)
                    menu.Add("[A] Attack");

                //if (room is IBossRoom)
                //    menu.Add("[B] Fight Boss");

                if (room is IInteractableRoom)
                    menu.Add("[E] Explore");

                if (room is ILockedRoom lockedRoom && lockedRoom.IsLocked == true)
                    menu.Add("[U] Unlock Door");

                menu.Add("[I] Inventory");
                menu.Add("[H] Hero Stats");
                menu.Add("[M] Map");
                menu.Add("[X] Exit");
            }
            else if (MenuState == "explore")
            {
                menu.Add("[I] Intelligence");
                menu.Add("[S] Strength");
                menu.Add("[A] Agility");
            }
            else if (MenuState == "unlock")
            {
                if (_roomManager.Room is Dungeon dungeon)
                {
                    if (dungeon.StoneGrabbed)
                    {
                        menu.Add("[S] Stone");
                    }
                    if (dungeon.KeyFormed)
                    {
                        menu.Add("[K] Key");
                    }
                    if (dungeon.CrackFound)
                    {
                        menu.Add("[C] Crack");
                    }
                }

                menu.Add("[F] Force");
            }
            else if (MenuState == "inventory")
            {
                menu.Add("[I] Items");
                menu.Add("[E] Equipment");
                menu.Add("[B] Back");
            }
            else if (MenuState == "items")
            {
                menu.Add("[V] View");
                menu.Add("[M] Manage");
                menu.Add("[B] Back");

            }
            else if (MenuState == "equipment")
            {
                menu.Add("[E] Equipped");
                menu.Add("[A] Available");
                menu.Add("[B] Back");
            }
            else if (MenuState == "manage")
            {
                menu.Add("[Q] Query");
                menu.Add("[S] Sort");
                menu.Add("[B] Back");
            }
            else if (MenuState == "sort")
            {
                menu.Add("[N] Name");
                menu.Add("[V] Value");
                menu.Add("[W] Weight");
            }
            else if (MenuState == "a/d")
            {
                menu.Add("[A] Ascending");
                menu.Add("[D] Descending");
            }
            else if (MenuState == "map")
            {
                var paths = _roomManager.GetAvailablePaths();
                foreach (var direction in paths.Keys)
                {
                    menu.Add($"[{direction[0]}] {direction}");
                }
            }

                return menu;
        }


        public static void Render(Player player, Room room, List<string> eventLog)
        {
            Console.Clear();
            int MaxLogLines = 8;
            var menu = BuildMenu(room);

            // Trim log
            var logLines = eventLog
                .TakeLast(MaxLogLines)
                .ToList();

            // Header
            Console.WriteLine(RenderHeroLine(player));
            Console.WriteLine(RenderRoomLine(room), ConsoleColor.White);

            Console.WriteLine("\n--- LOG ---------------------------------------------------------------------------");

            if (logLines.Count == 0)
            {
                Console.WriteLine("* Nothing has happened yet...");
            }
            else
            {
                foreach (var line in logLines)
                    Console.WriteLine($"* {line}");
            }

            Console.WriteLine("-----------------------------------------------------------------------------------");
            if(MenuState == "query")
            {
                Console.Write(">> ");
                return;
            }
            foreach (var option in menu)
                Console.Write(option + " ", ConsoleColor.White);
            Console.Write("\n>> ");
        }

        private static string RenderHeroLine(Player p)
        {
            var hpColor = GetHpColor(p.Health, p.MaxHealth);
            string weaponName = p.Equipped.TryGetValue(EquipmentSlot.Weapon, out var weapon)
            ? weapon.Name
            : "None";

            return $"[ HERO ] {p.Name} the {p.classType.ToString()}   " +
                   $"LV {p.Level}   " +
                   $"HP {hpColor}{p.Health}/{p.MaxHealth}\u001b[0m   " +
                   $"WPN: {weaponName}";
        }

        private static string RenderRoomLine(Room r)
        {
            return $"[ ROOM ] {r.Name} — {r.Description}";
        }

        // ───────────────────────────────────────────────────────
        // HP COLORING
        // ───────────────────────────────────────────────────────
        private static string GetHpColor(int hp, int max)
        {
            double pct = (double)hp / max;

            if (pct >= 0.7)
                return "\u001b[32m"; // green
            if (pct >= 0.3)
                return "\u001b[33m"; // yellow
            return "\u001b[31m";     // red
        }


    }
   
}
