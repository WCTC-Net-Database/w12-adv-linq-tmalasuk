using ConsoleRpgEntities.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Rooms.Interfaces
{
    public interface IRewardRoom
    {
        void GiveReward(Player player);
    }

}
