using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities
{
    // Assuming Player, ITargetable, and StatusEffect are defined elsewhere
    public class NullifyingAegis : Ability
    {
        public NullifyingAegis()
        {
            
        }

        public override int Activate(Player user)
        {
            return user.Agility * 2;
        }
    }
}