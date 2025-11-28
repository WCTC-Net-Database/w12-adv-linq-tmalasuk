using ConsoleRpg.Helpers.Battle;
using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpg.Helpers.Environments;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Data.Seeding;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Containers;
using ConsoleRpgEntities.Models.Equipments;
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
using static ConsoleRpgEntities.Models.Enums;

namespace ConsoleRpg.Helpers.Menus
{
    public class GameLoopMenu
    {

        public static OutputManager _outputManager;
        private readonly InventoryMenu _inventoryMenu;
        private readonly PlayerMenu _playerMenu;
        private readonly BattleManager _battleManager;
        private readonly MonsterManager _monsterManager;
        public static RoomManager RoomManager;
        private readonly PlayerManager _playerManager;
        private readonly GameContext _context;
        public static List<string> EventLog;
        public static string MenuState { get; set; }

        public GameLoopMenu(OutputManager outputManager, InventoryMenu inventoryMenu, PlayerMenu playerMenu, BattleManager battleManager, MonsterManager monsterManager, RoomManager roomManager, PlayerManager playerManager, GameContext context)
        {
            _outputManager = outputManager;
            _inventoryMenu = inventoryMenu;            
            _playerMenu = playerMenu;
            _battleManager = battleManager;
            _monsterManager = monsterManager;
            RoomManager = roomManager;
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
                SetMenuStateandRefresh("default");
                int underMenu = Console.GetCursorPosition().Top;
                var input = Console.ReadLine().ToLower();

                if (input == "d")
                {
                    List<string> messages = RoomManager.HandleDroppedLoot(_playerManager.Player, _inventoryMenu);
                    foreach (string msg in messages)
                    {
                        EventLog.Add(msg);
                    }
                }
                else if (input == "a")
                {
                    var battleMessages = RoomManager.SetUpAttack(this, _playerManager.Player, _inventoryMenu.InventoryManager);
                    foreach (string m in battleMessages)
                    {
                        EventLog.Add(m);
                    
                    }
                }
                else if (input == "e")
                {
                    _playerManager.Player.ExploredRooms.Add(RoomManager.Room);
                    RoomManager.HandleExploration(this, _playerManager.Player, _outputManager, underMenu);      
                }
                else if (input == "u")
                {
                    RoomManager.HandleUnlock(this, underMenu, _outputManager, _playerManager.Player);
                }
                else if (input == "i")
                {
                    _inventoryMenu.HandleInventorySelection(this, _outputManager, _playerManager.Player);
                }
                else if (input == "h")
                {
                    _playerMenu.MainMenu(this);
                }
                else if (input == "m") {

                    MenuState = "map";
                    Render(_playerManager.Player, RoomManager.Room, EventLog);
                    var direction = Console.ReadLine().ToLower();
                    List<string> messages = new List<string>();
                    switch (direction) {
                        case "n": foreach (string line in RoomManager.GoDirection("North", _playerManager.Player)) {messages.Add(line);}; break;
                        case "e": foreach (string line in RoomManager.GoDirection("East", _playerManager.Player)) { messages.Add(line); }; break;
                        case "s": foreach (string line in RoomManager.GoDirection("South", _playerManager.Player)) { messages.Add(line); }; break;
                        case "w": foreach (string line in RoomManager.GoDirection("West", _playerManager.Player)) { messages.Add(line); }; break;
                        case "u": foreach (string line in RoomManager.GoDirection("Up", _playerManager.Player)) { messages.Add(line); }; break;
                        case "d": foreach (string line in RoomManager.GoDirection("Down", _playerManager.Player)) { messages.Add(line); }; break;
                        default: messages.Add("Invalid Input"); break;
                    }
                    foreach (string line in messages) { EventLog.Add(line); }
                    setInstance();
                }
                else if (input == "x")
                {
                    _context.SaveChanges();
                    break;
                }                 

            }
        }


        private void setInstance()
        {
            Room room;

            if (_playerManager.Player.CurrentRoom != null)
            {
                // Make sure we include DroppedLoot and Items
                room = _context.Rooms
                    .Include(r => r.DroppedLoot)
                        .ThenInclude(dl => dl.Items)
                    .First(r => r.Id == _playerManager.Player.CurrentRoom.Id);
            }
            else
            {
                room = _context.Rooms
                    .Include(r => r.DroppedLoot)
                        .ThenInclude(dl => dl.Items)
                    .FirstOrDefault(r => r.Id == 1);
            }

            // If somehow DroppedLoot does not exist in DB, create it safely
            if (room.DroppedLoot == null)
            {
                room.DroppedLoot = new RoomItems
                {
                    Room = room,
                    Items = new List<Item>()
                };
                _context.RoomItems.Add(room.DroppedLoot); // explicitly add to DbContext
                _context.SaveChanges();
            }

            RoomManager.Room = room;
        }


        public void SetMenuStateandRefresh(string menuState)
        {
            MenuState = menuState.ToLower();
            Render(_playerManager.Player, RoomManager.Room, EventLog);
        }
        private List<string> BuildMenu(Room room)
        {
            var menu = new List<string>();

            // Always available
            if (MenuState == "default")
            {
                if (room.DroppedLoot.Items.Count > 0)
                {
                    menu.Add("[D] Dropped Loot");
                }
                if (room is ICombatRoom && room.MonstersInRoom.Count > 0)
                    menu.Add("[A] Attack");

                //if (room is IBossRoom)
                //    menu.Add("[B] Fight Boss");

                if (room is IInteractableRoom && !_playerManager.Player.ExploredRooms.Contains(room))
                    menu.Add("[E] Explore");

                if (room is ILockedRoom lockedRoom && lockedRoom.IsLocked == true)
                    menu.Add("[U] Unlock Door");

                menu.Add("[I] Inventory");
                menu.Add("[H] Hero");
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
                if (RoomManager.Room is Dungeon dungeon)
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
            else if (MenuState == "items")
            {
                menu.Add("[V] View");
                menu.Add("[M] Manage");
                menu.Add("[B] Back");

            }
            else if (MenuState == "manage")
            {
                menu.Add("[Q] Query");
                menu.Add("[S] Sort");
                menu.Add("[B] Back");
            }
            else if (MenuState == "query")
            {
                menu.Add(">> ");
            }
            else if (MenuState == "sort")
            {
                menu.Add("[N] Name");
                menu.Add("[V] Value");
                menu.Add("[W] Weight");
                menu.Add("[T] Type");
            }
            else if (MenuState == "a/d")
            {
                menu.Add("[A] Ascending");
                menu.Add("[D] Descending");
            }
            else if (MenuState == "hero")
            {
                menu.Add("[P] Profile");
                menu.Add("[G] Gear");
                menu.Add("[A] Attributes");
                menu.Add("[S] Spells");
                menu.Add("[B] Back");
            }
            else if (MenuState == "gear")
            {
                menu.Add("[E] Equipped");
                menu.Add("[A] Available");
                menu.Add("[B] Back");
            }
            else if (MenuState == "map")
            {
                var paths = RoomManager.GetAvailablePaths();
                foreach (var direction in paths.Keys)
                {
                    menu.Add($"[{direction[0]}] {direction}");
                }
            }

                return menu;
        }


        private void Render(Player player, Room room, List<string> eventLog)
        {
            _outputManager.Clear();
            int MaxLogLines = 8;
            var menu = BuildMenu(room);

            // Trim log
            var logLines = eventLog
                .TakeLast(MaxLogLines)
                .ToList();

            // Header
            _outputManager.WriteLine(RenderHeroLine(player));
            _outputManager.WriteLine(RenderRoomLine(room), ConsoleColor.White);

            _outputManager.WriteLine("\n--- LOG ---------------------------------------------------------------------------");

            if (logLines.Count == 0)
            {
                _outputManager.WriteLine("* Nothing has happened yet...");
            }
            else
            {
                foreach (var line in logLines)
                    _outputManager.WriteLine($"* {line}");
            }

            _outputManager.WriteLine("-----------------------------------------------------------------------------------");
            foreach (var option in menu) _outputManager.Write(option + " ", ConsoleColor.White);
            _outputManager.Display();
        }

        private static string RenderHeroLine(Player p)
        {
            var hpColor = GetHpColor(p.Health, p.MaxHealth);
            var mpColor = GetHpColor(p.Mana, p.MaxMana);
            string weaponName = p.Equipped.Weapon != null ? p.Equipped.Weapon.Name : "None";

            return $"[ HERO ] {p.Name} the {p.classType.ToString()}   " +
                   $"LV {p.Level}   " +
                   $"HP {hpColor}{p.Health}/{p.MaxHealth}\u001b[0m   " +
                   $"MP {hpColor}{p.Mana}/{p.MaxMana}\u001b[0m   " +
                   $"WPN: {weaponName}";
        }

        public void AddEventsandRefresh(List<string> events)
        {
            foreach (string e in events)
            {
                EventLog.Add(e);
            }
            Render(_playerManager.Player, RoomManager.Room, EventLog);
        }

        public void AddEventandRefresh(string e)
        {
            EventLog.Add(e);
            Render(_playerManager.Player, RoomManager.Room, EventLog);
        }

        private static string RenderRoomLine(Room r)
        {
            return $"[ ROOM ] {r.Name} — {r.Description}";
        }
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
