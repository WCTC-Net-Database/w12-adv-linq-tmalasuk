using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Models.Rooms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.Battle
{
    public class RoomManager
    {
        public Room Room { get; set; }
        public RoomManager()
        {

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
    }

}
