using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Rooms.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Rooms
{
    public abstract class Room : IRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoomType { get; set; }
        [NotMapped] public Room? North { get; set; }
        [NotMapped] public Room? South { get; set; }
        [NotMapped] public Room? East { get; set; }
        [NotMapped] public Room? West { get; set; }
        [NotMapped] public Room? Up { get; set; }
        [NotMapped] public Room? Down { get; set; }
        public List<Player> PlayersInRoom { get; set; } = new();
        public List<Monster> MonstersInRoom { get; set; } = new();

        public Dictionary<string, Room> GetAvailablePaths()
        {
            var paths = new Dictionary<string, Room>();

            if (North != null) paths["North"] = North;
            if (South != null) paths["South"] = South;
            if (East != null) paths["East"] = East;
            if (West != null) paths["West"] = West;
            if (Up != null) paths["Up"] = Up;
            if (Down != null) paths["Down"] = Down;

            return paths;
        }

    }
}
