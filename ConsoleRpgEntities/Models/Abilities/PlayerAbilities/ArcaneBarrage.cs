using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities
{
    // Assuming Player and ITargetable are defined elsewhere
    public class ArcaneBarrage : Ability
    {
        public ArcaneBarrage()
        {
            
        }

        public override int Activate(Player user)
        {
            user.Mana -= this.ManaCost;
            return user.Intelligence * 5;
        }
    }
}