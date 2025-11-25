using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities
{
    // Assuming Player and ITargetable are defined elsewhere
    public class SiphoningStrike : Ability
    {

        public SiphoningStrike()
        {
            
        }

        public override int Activate(Player user)
        {
            user.Mana -= ManaCost;
            return (user.Intelligence + user.Strength + user.Agility) * 2;
        }
    }
}
