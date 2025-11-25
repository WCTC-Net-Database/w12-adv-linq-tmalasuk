using ConsoleRpgEntities.Models.Characters.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Rooms.Interfaces
{
    public interface IBossRoom
    {
        public bool IsDefeated { get; }
        public Monster Boss { get;  }
    }
}
