using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpg.Helpers.Environments;
using ConsoleRpg.Helpers.Menus;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Models.Rooms.Interfaces;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.Battle
{
    public class RoomManager
    {
        public Room Room { get; set; }
        public static MonsterManager MonsterManager { get; set; }
        public static BattleManager BattleManager { get; set; }
        public RoomManager(MonsterManager monsterManager, BattleManager battleManager)
        {
            MonsterManager = monsterManager;
            BattleManager = battleManager;
        }

        public void HandleExploration(GameLoopMenu menu, Player player, OutputManager outputManager, int belowMenu)
        {
            menu.SetMenuStateandRefresh("explore");
            var explorMsgs = ExploreRoom(player);
            menu.AddEventsandRefresh(explorMsgs);
            while (true)
            {
                var input = Console.ReadLine()?.ToLower();
                if (input == "i")
                {
                    var intMessages = InteractWithAttribute(player, "intelligence");
                    menu.AddEventsandRefresh(intMessages);
                    break;
                }
                else if (input == "s")
                {
                    var strMessages = InteractWithAttribute(player, "strength");
                    menu.AddEventsandRefresh(strMessages);
                    break;
                }
                else if (input == "a")
                {
                    var agilMessages = InteractWithAttribute(player, "agility");
                    menu.AddEventsandRefresh(agilMessages);
                    break;
                }
                else
                {
                    menu.AddEventandRefresh("Invalid Input.");
                }
            }
            menu.SetMenuStateandRefresh("default");
        }

        public void HandleUnlock(GameLoopMenu menu, int belowMenu, OutputManager outputManager, Player player)
        {
            menu.SetMenuStateandRefresh("unlock");
            while (true)
            {
                var choice = Console.ReadLine().ToLower();

                if (Room is ILockedRoom lockedRoom)
                {
                    menu.AddEventsandRefresh(lockedRoom.TryUnlock(choice, player));
                    break;
                }
                else
                {
                    menu.AddEventandRefresh("Invalid Input.");
                }             
            }
            menu.SetMenuStateandRefresh("default");
        }
          
        public List<String> ExploreRoom(Player player)
        {
            if (Room is IInteractableRoom interactableRoom)
            {
                return interactableRoom.Interact(player);
            }
            return new List<string> { "There is nothing to interact with here." };
        }

        internal List<string> InteractWithAttribute(Player player, string v)
        {
            if  (Room is IInteractableRoom interactableRoom)
            {
                if (v == "intelligence")
                {
                    var message = interactableRoom.IntelligenceInteraction(player);
                    return message;
                }
                else if (v == "strength")
                {
                    var message = interactableRoom.StrengthInteraction(player);
                    return message;
                }
                else if (v == "agility")
                {
                    var message = interactableRoom.AgilityInteraction(player);
                    return message;
                }
            }
            return new List<string> { "There is nothing to interact with here." };

        }

        public Dictionary<string, Room> GetAvailablePaths()
        {
            return Room.GetAvailablePaths();
        }

        public List<string> GoDirection(string direction, Player player)
        {
            var messages = new List<string>();
            messages.Add($"You attempt to go {direction}...");
            var leavingRoom = Room;
            switch (direction) {
                case "North": Room = Room.North; break;
                case "East": Room = Room.East; break;
                case "South": Room = Room.South; break;
                case "West": Room = Room.West; break;
                case "Up": Room = Room.Up; break;
                case "Down": Room = Room.Down; break;
                default: break;
            }
            if (Room is null || (leavingRoom is ILockedRoom lockedRoom && lockedRoom.IsLocked))
            {
                if (leavingRoom == null)
                {
                    messages.Add("There is no room in that direction.");
                }
                else
                {
                    messages.Add("The door won't budge.");
                }
                Room = leavingRoom;
                return messages;
            }
            else
            {
                player.CurrentRoom = Room;
                messages.Add($"You wander cautiously into {Room.RoomType}");
                return messages;
            }
            
        }

        public List<string> SetUpAttack(GameLoopMenu menu, Player player, InventoryManager inventoryManager)
        {
            var messages = new List<string>();
            var monster = Room.MonstersInRoom.FirstOrDefault();
            if (monster == null)
            {
                menu.AddEventandRefresh("There are no monsters left in this room");
                return messages;
            }
            MonsterManager.Monster = monster;

            BattleManager.BattleLoop(menu);

            var endMessages = BattleManager.HandleMonsterDefeat(player, MonsterManager.Monster, inventoryManager, Room);
            Room.MonstersInRoom.Remove(monster);
            foreach (string line in endMessages)
            {
                messages.Add(line);
            }
            return messages;

        }

        internal List<string> HandleDroppedLoot(Player player, InventoryMenu inventory)
        {
            List<Item> items = Room.DroppedLoot.Items.ToList();
            return inventory.ItemListInteract(items);
        }
    }

}
