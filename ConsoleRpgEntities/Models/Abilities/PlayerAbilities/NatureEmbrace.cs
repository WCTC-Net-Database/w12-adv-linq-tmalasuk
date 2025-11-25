using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities
{
    
    public class NatureEmbrace : Ability
    {
        public int Stacks { get; set; } = 0;

        public NatureEmbrace()
        {
            
        }

        public override int Activate(Player user)
        {
            Stacks += 3;
            return 0;
        }

        public int SecondaryHeal(Player user)
        {
            return (user.Intelligence * 2) * Stacks;
        }
    }
}