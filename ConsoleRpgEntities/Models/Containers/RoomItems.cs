using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Containers
{
    public class RoomItems : IItemContainer
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        
    }
}
